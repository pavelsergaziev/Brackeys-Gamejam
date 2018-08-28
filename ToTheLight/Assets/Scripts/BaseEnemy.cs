using System.Collections;
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
    
    

    
}
