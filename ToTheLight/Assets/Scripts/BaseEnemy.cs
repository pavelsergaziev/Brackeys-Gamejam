﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
//[DefaultExecutionOrder(+1)]
public abstract class BaseEnemy : MonoBehaviour {
    
    public float patrolSpeed;
    
    public float agroRadius;

    protected Transform _player;
    protected virtual void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerScript = other.GetComponent<PlayerController>();
        if (playerScript != null)
        {
            FindObjectOfType<GameStateManager>().GameOverLoss();
        }
    }


}
