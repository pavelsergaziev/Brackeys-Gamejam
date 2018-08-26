using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    public float jumpforce;
    public float moveSpeed;
    public PlayerCondition playerCondition;
    
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        
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
}

