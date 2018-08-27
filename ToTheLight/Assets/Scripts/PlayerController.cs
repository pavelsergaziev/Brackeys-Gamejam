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


    private const float rbMass = 0.5f;
    private const float rbGravityScale = 0.5f;
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        if (!setRBValuesByEditor)
        {
            SetRbValues();
        }
        
    }

    // Update is called once per frame
    void Update ()
    {
        switch (playerCondition)
        {
            case PlayerCondition.bug:
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
        var horizontal = Input.GetAxisRaw("Horizontal");
        var jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            _playerRb.velocity = Vector2.zero;
            _playerRb.AddForce(Vector2.up * jumpforce);
        }
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * moveSpeed);
    }

    void SetRbValues()
    {
        _playerRb.mass = rbMass;
        _playerRb.gravityScale = rbGravityScale;
    }
}

