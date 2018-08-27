using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer))]
public class Spider : BaseEnemy
{
    public float dropSpeed;
    private List<Vector3> _patrolPoints;
    private int _patrolPointIndex = 0;
    private bool _isPatroling = true;
    private TrailRenderer _web;

    protected override void Start()
    {
        base.Start();
        _web = GetComponent<TrailRenderer>();
        _web.enabled = false;
        if (_patrolPoints == null)
        {
            _patrolPoints = new List<Vector3>
            {
                new Vector3(transform.position.x-1,transform.position.y),
                new Vector3(transform.position.y+1, transform.position.y)
            };
        }
    }
    
    void Patroling()
    {
        if (_isPatroling)
        {
            Vector2 destination = _patrolPoints[_patrolPointIndex];

            if (Vector2.Distance(transform.position, destination) > 0.01f)
            {

                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * patrolSpeed);
            }
            else
            {
                _patrolPointIndex++;
                if (_patrolPointIndex == _patrolPoints.Count)
                {
                    _patrolPointIndex = 0;
                }
            }
        }
        
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < agroRadius)
        {
            _isPatroling = false;
            _web.enabled = true;
            Drop();
        }
        else
        {
            Patroling();
        }
        
    }
    void Drop()
    {
        
        transform.position += Vector3.down * Time.deltaTime * dropSpeed;
    }
}
