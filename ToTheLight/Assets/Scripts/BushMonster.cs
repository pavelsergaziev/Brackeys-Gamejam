using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushMonster : BaseEnemy {

    private bool _isHidden;
    private Animator _animator;
    [SerializeField]
    private float _followSpeed;
    [SerializeField]
    private float _distanceToPlayerToEmerge;
    [SerializeField]
    private float _timeBeforeHidingAgain;

    private WaitForSeconds _waitToHide;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _isHidden = true;
        _animator = GetComponent<Animator>();
        _waitToHide = new WaitForSeconds(_timeBeforeHidingAgain);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_isHidden)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < _distanceToPlayerToEmerge)
                _animator.SetBool("Activate", true);
        }

        else if (Vector3.Distance(transform.position, _player.transform.position) < agroRadius)
        {
            StopAllCoroutines();
            FollowPlayer();
        }

        else
            StartCoroutine("WaitAndHide");
    }
    

    public void StartMovingAfterEmerging()
    {
        _isHidden = false;
    }

    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _followSpeed);
    }

    private IEnumerator WaitAndHide()
    {
        yield return _waitToHide;
        _isHidden = true;
        _animator.SetBool("Activate", false);
    }
}
