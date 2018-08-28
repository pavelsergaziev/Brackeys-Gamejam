using System.Collections;
using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    private Animator _animator;

    [SerializeField]
    private float _minBlinkDelay;
    [SerializeField]
    private float _maxBlinkDelay;
    
    private bool _doBlink;

    private bool _canStopMoving;

    private Coroutine _blinkDelayCoroutine;
    private bool _coroutineIsRunning;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _canStopMoving = true;
        _coroutineIsRunning = false;
    }

    public void PlayAnimation(bool playerIsMoving)
    {
        if (playerIsMoving)
        {
            if (_coroutineIsRunning)
            {
                StopCoroutine(_blinkDelayCoroutine);
                _coroutineIsRunning = false;
            }
            
            _animator.Play("Moth Move");
        }
        else if (_doBlink)
        {
            _animator.Play("Moth Blink");
            _blinkDelayCoroutine = StartCoroutine("BlinkAnimationCountDown");
        }
        else if (_canStopMoving)
        {
            _animator.Play("Moth Idle");
            
            if(!_coroutineIsRunning)
                _blinkDelayCoroutine = StartCoroutine("BlinkAnimationCountDown");
        }
    }

    private IEnumerator BlinkAnimationCountDown()
    {
        _coroutineIsRunning = true;
        _doBlink = false;

        yield return new WaitForSeconds(UnityEngine.Random.Range(_minBlinkDelay, _maxBlinkDelay));

        _doBlink = true;
        _coroutineIsRunning = false;
    }

    public void AnimationEvent(int obligatoryAnimComplete)
    {
        _canStopMoving = Convert.ToBoolean(obligatoryAnimComplete);
    }

}
