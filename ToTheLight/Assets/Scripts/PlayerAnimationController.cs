using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationController : MonoBehaviour {

    private Animator _animator;

    private ScreenLighting _screenLighting;

    [SerializeField]
    private float _minBlinkDelay;
    [SerializeField]
    private float _maxBlinkDelay;
    
    private bool _doBlink;

    private bool _isBlinking;
    private bool _canStopMoving;

    private Coroutine _blinkDelayCoroutine;
    private bool _coroutineIsRunning;

    private int _evolutionStage;
    private string _idleStateName;
    private string _blinkStateName;
    private string _moveStateName;

    [SerializeField]
    private GameObject _colliderToTurnOnAfterTransformation;
    [SerializeField]
    private GameObject _colliderToTurnOffAfterTransformation;

    [SerializeField]
    private Vector2 _mothColliderOffset, _mothColliderSize;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _screenLighting = FindObjectOfType<ScreenLighting>();

        _canStopMoving = true;
        _coroutineIsRunning = false;

        ResetEvolution();
    }

    
    public void PlayAnimation(bool playerIsMoving)
    {
        if (_evolutionStage > 1 && _evolutionStage < 4) //если мы в коконе, то не играть другие анимации.
            return;

        if (playerIsMoving)
        {
            if (_coroutineIsRunning)
            {
                StopCoroutine(_blinkDelayCoroutine);
                _coroutineIsRunning = false;
            }
            _animator.Play(_moveStateName);
        }
        else if (_doBlink)
        {
            _animator.Play(_blinkStateName);
            _blinkDelayCoroutine = StartCoroutine("BlinkAnimationCountDown");
        }
        else if (_canStopMoving && !_isBlinking)
        {
            _animator.Play(_idleStateName);
            
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

    public void MoveComplete (int obligatoryAnimComplete)
    {
        _canStopMoving = Convert.ToBoolean(obligatoryAnimComplete);
    }

    public void IsBlinking(int isBlinking)
    {
        _isBlinking = Convert.ToBoolean(isBlinking);
    }
    

    public void SwitchEvolutionStage()
    {
        switch (++_evolutionStage)
        {
            case 1:
                {
                    _idleStateName = "Caterpillar Idle";
                    _blinkStateName = "Caterpillar Blink";
                    _moveStateName = "Caterpillar Move";
                    break;
                }
            case 2:// играем анимацию, дальше либо в конце её на триггер вешаем вызов SwitchEvolutionStage(), либо запускаем таймер, либо ждём инпута от плэерконтроллера или менеджера скриптов
                {
                    _animator.Play("CaterpillarToCocoon");

                    _screenLighting.ChangeStateToTransforming();

                    _colliderToTurnOffAfterTransformation.SetActive(false);
                    _colliderToTurnOnAfterTransformation.SetActive(true);

                    break;
                }
            case 3:// играем анимацию, дальше либо в конце её на триггер вешаем вызов SwitchEvolutionStage(), либо запускаем таймер, либо ждём инпута от плэерконтроллера или менеджера скриптов
                {
                    _animator.Play("CocoonToButterfly");

                    var collider = transform.GetComponent<BoxCollider2D>();
                    collider.size = _mothColliderSize;
                    collider.offset = _mothColliderOffset;

                    _screenLighting.ChangeStateToDependingOnDistanceToLight();

                    break;
                }
            case 4:
                {
                    _idleStateName = "Moth Idle";
                    _blinkStateName = "Moth Blink";
                    _moveStateName = "Moth Move";
                    break;
                }
            default:
                break;
        }        
    }

    public void ResetEvolution()
    {
        _evolutionStage = 0;
        SwitchEvolutionStage();
    }

}
