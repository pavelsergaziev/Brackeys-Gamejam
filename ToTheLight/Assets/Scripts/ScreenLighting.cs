using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLighting : MonoBehaviour
{    
    private Transform _player;
    private Image _darkness;
    private WaitForSeconds _delay;

    [SerializeField]
    private Transform _mainLight;
    [SerializeField]
    private float _lightChangeDelay;
    [SerializeField]
    private float _minAlpha;
    [SerializeField]
    private float _maxAlpha;
    [SerializeField]
    private float _startingDistanceToLight;
    [SerializeField]
    private float _alphaCorrectionValue;

    private Color _nightColor;
    private float _transformationTime;

    // Use this for initialization
    void Start()
    {
        _darkness = transform.GetComponent<Image>();

        var playerController = FindObjectOfType<PlayerController>();
        _player = playerController.transform;
        _transformationTime = playerController.transformationTime;

        _nightColor = new Color(0, 0, 0, _maxAlpha);

        _delay = new WaitForSeconds(_lightChangeDelay);
    }

    private IEnumerator ChangeLighting()
    {
        while (true)
        {
            float targetAlpha = (Vector3.Distance(_player.position, _mainLight.position) * _alphaCorrectionValue) / _startingDistanceToLight;

            if (targetAlpha > _maxAlpha)
                targetAlpha = _maxAlpha;
            else if (targetAlpha < _minAlpha)
                targetAlpha = _minAlpha;

            _darkness.color = new Color(_darkness.color.r, _darkness.color.g, _darkness.color.b, targetAlpha);

            yield return _delay;
        }
    }

    public void ChangeStateToDependingOnDistanceToLight()
    {
        StartCoroutine("ChangeLighting");
    }

    public void ChangeStateToTransforming()
    {
        StartCoroutine("Transformation");
    }

    private IEnumerator Transformation()
    {
        float currentTime = Time.time;
        float targetTime = currentTime + _transformationTime;

        while(currentTime <= targetTime)
        {
            var deltatime = Time.deltaTime;
            currentTime += deltatime;

            yield return new WaitForEndOfFrame();

            _darkness.color = Color.Lerp(_darkness.color, _nightColor, deltatime);            
        }

        _darkness.color = _nightColor;
    }
}
