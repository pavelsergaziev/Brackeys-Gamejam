using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothForTitleScreen : MonoBehaviour {

    private List<Vector3> _patrolPoints;
    private int _patrolPointIndex = 0;
    public float _patrolSphereRadious;
    public float _patrolSpeed;

    private const int PatrolPointsCount = 10;

    // Use this for initialization
    void Start () {

        _patrolPoints = new List<Vector3>();

        for (int i = 0; i < PatrolPointsCount; i++)
        {
            _patrolPoints.Add(Random.insideUnitCircle * _patrolSphereRadious + (Vector2)transform.position);
        }

    }
	
	// Update is called once per frame
	void Update () {

        Vector2 destination = _patrolPoints[_patrolPointIndex];

        if (Vector2.Distance(transform.position, destination) > 0.01f)
        {

            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * _patrolSpeed);
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
