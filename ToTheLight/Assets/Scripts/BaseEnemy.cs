using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[DefaultExecutionOrder(+1)]
public abstract class BaseEnemy : MonoBehaviour {
    [Range(0,100)]
    public float patrolSpeed;
    public float followSpeed;

    protected Transform _player;
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }
    
    

    
}
