using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadFly : BaseEnemy
{
    private List<Vector3> _patrolPoints;
    private int _patrolCount = 0;
    private void Start()
    {
        _patrolPoints = new List<Vector3>();
        for (int i = 0; i < 6; i++)
        {
            _patrolPoints.Add(Random.insideUnitCircle * 1);
           
            
        }
    }
    public void Update()
    {
        //Patroling();
    }
    void Patroling()
    {
        Vector3 destination = _patrolPoints[_patrolCount];
        Debug.Log(_patrolCount);
        if (Vector3.Distance(transform.position, destination) >0.09f)
        {
            transform.Translate(destination * Time.deltaTime*patrolSpeed);
        }
        else
        {
            _patrolCount++;
            if (_patrolCount == _patrolPoints.Count)
            {
                _patrolCount = 0;
            }
        }
        
    }
}
