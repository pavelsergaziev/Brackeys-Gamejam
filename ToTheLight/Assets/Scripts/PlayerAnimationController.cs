using System.Collections;
using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    private Animator _animator;

    [SerializeField]
    private float _minBlinkDelay;
    [SerializeField]
    private float _maxBlinkDelay;
    [SerializeField]
    private float _blinkAnimationSpeed;
    [SerializeField]
    private float _mothMoveAnimationSpeed;
    
    private bool _doBlink;

    private bool _isBlinking;
    private bool _canStopMoving;

    private Coroutine _blinkDelayCoroutine;
    private bool _coroutineIsRunning;

    private int _evolutionStage;
    private string _idleStateName;
    private string _blinkStateName;
    private string _moveStateName;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _canStopMoving = true;
        _coroutineIsRunning = false;

        _evolutionStage = 2;//ПОМЕНЯТЬ НА НОЛЬ
        SwitchEvolutionStage();
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

    //СПРОСИТЬ, КАК ОНО В ПЛЭЕРКОНТРОЛЛЕРЕ РАБОТАЕТ
    public void SwitchEvolutionStage(PlayerCondition evolutionStage)
    {
        switch (evolutionStage)
        {
            case PlayerCondition.bug:
                {

                    break;
                }
                
            case PlayerCondition.butterfly:
                {

                    break;
                }
            case PlayerCondition.uncontrollable:
                {
                    //
                    break;
                }
            default:
                break;
        }
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
            case 2://играем анимацию, дальше либо в конце её на триггер вешаем вызов SwitchEvolutionStage(), либо запускаем таймер, либо ждём инпута от плэерконтроллера или менеджера скриптов
                {

                    break;
                }
            case 3:
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

}
