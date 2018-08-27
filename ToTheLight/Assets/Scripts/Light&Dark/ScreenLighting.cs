using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLighting : MonoBehaviour {

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

    // Use this for initialization
    void Start ()
    {
        _darkness = transform.GetComponent<Image>();
        _player = FindObjectOfType<PlayerController>().transform;
        _delay = new WaitForSeconds(_lightChangeDelay);

        StartCoroutine("ChangeLighting");
	}
	
	private IEnumerator ChangeLighting()
    {
        while (true)
        {
            float targetAlpha = ((_player.position - _mainLight.position).magnitude * _alphaCorrectionValue) / _startingDistanceToLight;

            if (targetAlpha > _maxAlpha)
                targetAlpha = _maxAlpha;
            else if (targetAlpha < _minAlpha)
                targetAlpha = _minAlpha;

            _darkness.color = new Color(_darkness.color.r, _darkness.color.g, _darkness.color.b, targetAlpha);

            yield return _delay;
        }
    }
}
