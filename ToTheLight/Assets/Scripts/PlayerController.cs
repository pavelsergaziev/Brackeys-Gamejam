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
    public float transformationTime = 5;

    private const float rbMass = 0.5f;
    private const float rbGravityScale = 0.5f;
    private int _leafCount = 0;
    private bool _isTransformating = false;
    private bool _canClimb = false;

    private PlayerAnimationController _animation;


    
    private void Start()
    {
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
        
    }
    void ButterflyMove()
    {
        bool isMoving = false;

        var horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
            isMoving = true;

        var jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            _playerRb.velocity = Vector2.zero;
            _playerRb.AddForce(Vector2.up * jumpforce);

            isMoving = true;
        }
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * moveSpeed);

        if (_animation!=null)
        {
            _animation.PlayAnimation(isMoving);
        }
        else
        {
            throw new Exception("Отсутствует аниматор на обьекте "+ name);
        }
        
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
            _isTransformating = true;
            _leafCount = 0;
            playerCondition = PlayerCondition.uncontrollable;
            /// Animation
            yield return new WaitForSeconds(transformationTime);
            playerCondition = PlayerCondition.butterfly;            
            Debug.Log("Трансофрмация завершена");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Leaf")
        {
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

        if (collision.tag == "Climb"&&playerCondition == PlayerCondition.caterpillar)
        {
            _playerRb.isKinematic = false;
            _canClimb = false;
        }
    }

}

