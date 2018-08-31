using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : BaseEnemy
{
    public float dropSpeed;
    public List<Vector3> patrolPoints;
    private int _patrolPointIndex = 0;
    private bool _isPatroling = true;
    private TrailRenderer _web;
    private bool _isDead = false;
    public Trigger trigger;

    protected override void Start()
    {
        base.Start();
        _web = GetComponentInChildren<TrailRenderer>();
        if (_web == null)
        {
            var trailRendererGameObj = new GameObject { name = "TrailRenderer" };           
            trailRendererGameObj.transform.SetParent(transform);            
            _web = trailRendererGameObj.AddComponent<TrailRenderer>();
            trailRendererGameObj.transform.position = transform.position;
        }
        SetTrailRendereParams();
        
        if (patrolPoints.Count == 0)
        {
            patrolPoints = new List<Vector3>
            {
                new Vector3(transform.position.x-1,transform.position.y) ,
                new Vector3(transform.position.x+1, transform.position.y)
            };
        }
    }

    void Patroling()
    {
        Vector2 destination = patrolPoints[_patrolPointIndex];

        if (Vector2.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * patrolSpeed);
        }
        else
        {
            _patrolPointIndex++;
            if (_patrolPointIndex == patrolPoints.Count)
            {
                _patrolPointIndex = 0;
            }
        }
    }
    private void Update()
    {
        if (trigger.isTriggered)
        {
            _isPatroling = false;
        }
        if (_isPatroling)
        {
            Patroling();
        }
        else
        {
            Drop();
        }  
    }
    void Drop()
    {
        _soundManager.PlaySound("SpyderAttack", true);
        _web.enabled = true;
        transform.position += Vector3.down * Time.deltaTime * dropSpeed;
        Dead();

    }
    void Dead()
    {
        if (!_isDead)
        {
            Destroy(gameObject, 3);
            _isDead = true;
        }
    }
    void SetTrailRendereParams()
    {
        _web.material = (Material)Resources.Load("WebMaterial");
        _web.emitting = true;
        _web.time = 5;
        _web.startWidth = 0.019f;
        _web.enabled = false;
    }

    
}
