using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    public float jumpforce = 55;
    public float moveSpeed = 2;
    public PlayerCondition playerCondition;
    public bool setRBValuesByEditor = false;

    public float transformationTime = 7;
    public float transformationExitTime = 2;


    private const float rbMass = 0.5f;
    private const float rbGravityScale = 0.5f;
    private int _leafCount = 0;
    private bool _isTransformating = false;
    private bool _canClimb = false;
    private SoundManager _soundManager;

    private PlayerAnimationController _animation;
    private bool _isMoving;

    private Vector3 _localScaleFacingRight = new Vector3(1, 1, 1);
    private Vector3 _localScaleFacingLeft = new Vector3(-1, 1, 1);


    private void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _playerRb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<PlayerAnimationController>();

        if (!setRBValuesByEditor)
        {
            SetRbValues();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        _isMoving = false;

        if (_leafCount == 3)
        {
            StartCoroutine(Transformation());
        }
        switch (playerCondition)
        {  
            case PlayerCondition.caterpillar:
                CaterpillarMove();
                break;
            case PlayerCondition.butterfly:
                ButterflyMove();
                break;
            case PlayerCondition.uncontrollable:
                break;
            default:
                break;
        }

        if (_animation != null)
        {
            _animation.PlayAnimation(_isMoving);
        }
        else
        {
            throw new Exception("Отсутствует аниматор на обьекте " + name);
        }


    }
    void ButterflyMove()
    {     
        var horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
            _isMoving = true;

        var jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            _playerRb.velocity = Vector2.zero;
            _playerRb.AddForce(Vector2.up * jumpforce);

            _isMoving = true;
        }
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * moveSpeed);

    }
    void CaterpillarMove()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * moveSpeed);
        if (_canClimb)
        {
            transform.Translate(Vector2.up * vertical * Time.deltaTime * moveSpeed);
        }

        if (horizontal != 0 || (vertical !=0 && _canClimb))
            _isMoving = true;

        //разворот по горизонтали в зависимости от направления движения
        if (horizontal < 0)
            transform.localScale = _localScaleFacingLeft;
        else if (horizontal > 0)
            transform.localScale = _localScaleFacingRight;
    }

    void SetRbValues()
    {
        _playerRb.mass = rbMass;
        _playerRb.gravityScale = rbGravityScale;
    }
    IEnumerator Transformation()
    {
        
        if(!_isTransformating)
        {

            Debug.Log("Трансофрмация начата");
            _soundManager.Transformation();

            
            _isTransformating = true;
            _leafCount = 0;
            playerCondition = PlayerCondition.uncontrollable;

            // Animation
            _animation.SwitchEvolutionStage();
            
            yield return new WaitForSeconds(transformationTime);

            // Animation
            _animation.SwitchEvolutionStage();
            _soundManager.PlaySound("TransformationEnd");
            yield return new WaitForSeconds(transformationExitTime);
            playerCondition = PlayerCondition.butterfly;

            // Animation
            _animation.SwitchEvolutionStage();
            

            Debug.Log("Трансофрмация завершена");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Leaf")
        {

            _soundManager.SoundPitch("LeafEat", UnityEngine.Random.Range(0.8f, 1.2f));
            _soundManager.PlaySound("LeafEat");
            _leafCount++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Climb" && playerCondition == PlayerCondition.caterpillar)
        {
            _playerRb.isKinematic = true;
            _canClimb = true;
        }
    }
    private void OnTriggerExit2D (Collider2D collision)
    {

        if (collision.tag == "Climb" && playerCondition == PlayerCondition.caterpillar)
        {
            _playerRb.isKinematic = false;
            _canClimb = false;
        }
    }

}

