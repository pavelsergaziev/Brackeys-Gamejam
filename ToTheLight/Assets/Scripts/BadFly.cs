using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BadFly : BaseEnemy
{
    private List<Vector3> _patrolPoints;
    private int _patrolPointIndex = 0;
    public float patrolSphereRadious;
    public float followSpeed;

    private float _soundRange = 6;

    private const  int  _patrolPointsCount = 10;
    private Sound _beeFlySound;
    private AudioSource _audio;

    protected override void Start()
    {
        base.Start();

        
        _audio = GetComponent<AudioSource>();
        _audio.volume = 0f;
        _audio.Play();
        _patrolPoints = new List<Vector3>();
        for (int i = 0; i < _patrolPointsCount; i++)
        {
            _patrolPoints.Add(Random.insideUnitCircle * patrolSphereRadious + (Vector2)transform.position);
        }
    }
    private void Update()
    {
        SoundControl();
        if (Vector3.Distance(transform.position,_player.transform.position)<agroRadius)
        {
            FollowPlayer();
        }
        else
        {
            Patroling();
        }
    }
    void Patroling()
    {
        Vector2 destination = _patrolPoints[_patrolPointIndex];
        
        if (Vector2.Distance(transform.position, destination) >0.01f)
        {
            
           transform.position = Vector3.MoveTowards(transform.position, destination , Time.deltaTime*patrolSpeed);
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
    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * followSpeed);
    }
    
    void SoundControl()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < _soundRange)
        {

            if (_audio.volume <= .5f)
            {
                _audio.volume = Mathf.Lerp(_audio.volume, .5f, .05f);
            }

        }
        else
        {
            if (_audio.volume >= 0)
            {
                _audio.volume = Mathf.Lerp(_audio.volume, 0f, .05f);
            }
        }
    }
}
