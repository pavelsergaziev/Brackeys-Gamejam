﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    private PlayerController _playerController;
    private PlayerAnimationController _playerAnimationController;
    private ScreenLighting _screenLighting;
    private Image _screenFilterImage;
    private CameraController _camera;

    private bool _inPausedState;
    private bool _inGameOverMenu;


    [SerializeField]
    private GameObject _btnResume;
    [SerializeField]
    private GameObject _btnRestart;
    [SerializeField]
    private GameObject _btnQuit;
    [SerializeField]
    private Text _txtGameOverText;

    [SerializeField]
    private string _VictoryText;
    [SerializeField]
    private string _LossText;

    [SerializeField]
    private GameObject _deathBurstLoss;
    [SerializeField]
    private GameObject _deathBurstVictory;

    [SerializeField]
    private Color _pauseScreenColor;
    [SerializeField]
    private Color _victoryScreenColor;
    [SerializeField]
    private Color _lossScreenColor;

    [SerializeField]
    private float _screenColorChangeTime;

    private Color _savedUnpausedColor;

    private PlayerCondition _savedUnpausedPlayerCondition;

	// Use this for initialization
	void Start ()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerAnimationController = FindObjectOfType<PlayerAnimationController>();
        _screenLighting = FindObjectOfType<ScreenLighting>();
        _camera = FindObjectOfType <CameraController>();
        _screenFilterImage = _screenLighting.transform.GetComponent<Image>();

        _inGameOverMenu = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !_inGameOverMenu)
            SwitchPausedUnpaused();
    }

    public void SwitchPausedUnpaused()
    {
        if (_inPausedState)//выходим из паузы
        {
            _screenFilterImage.color = _savedUnpausedColor;
            _playerController.playerCondition = _savedUnpausedPlayerCondition;

            _btnResume.SetActive(false);
            _btnRestart.SetActive(false);
            _btnQuit.SetActive(false);

            Time.timeScale = 1;
        }
        else//ставим на паузу
        {
            _savedUnpausedColor = _screenFilterImage.color;
            _screenFilterImage.color = _pauseScreenColor;

            _savedUnpausedPlayerCondition = _playerController.playerCondition;
            _playerController.playerCondition = PlayerCondition.uncontrollable;

            _btnResume.SetActive(true);
            _btnRestart.SetActive(true);
            _btnQuit.SetActive(true);

            Time.timeScale = 0;
        }

        _inPausedState = !_inPausedState;
    }


    public void GameOverVictory()
    {
        GameOver(_deathBurstVictory, _victoryScreenColor, _VictoryText);        
    }

    public void GameOverLoss()
    {
        GameOver(_deathBurstLoss, _lossScreenColor, _LossText);
    }

    private void GameOver(GameObject deathburst, Color screenColor, string gameOverText)
    {
        _inGameOverMenu = true;

        _screenLighting.StopAllCoroutines();
        _screenLighting.enabled = false;

        Instantiate(deathburst, _playerController.transform.position, Quaternion.identity);
        _playerController.gameObject.SetActive(false);

        StartCoroutine("ChangeScreenColor", screenColor);
        _txtGameOverText.text = gameOverText;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //ПЕРЕПРОВЕРИТЬ!! Работает коряво
    private IEnumerator ChangeScreenColor(Color targetColor)
    {
        float startingTime = Time.time;
        float currentTime = startingTime;
        float targetTime = startingTime + _screenColorChangeTime;
        while (currentTime <= targetTime)
        {
            currentTime = Time.time;

            yield return new WaitForEndOfFrame();
            
            _screenFilterImage.color = Color.Lerp(_screenFilterImage.color, targetColor, (currentTime - startingTime) / _screenColorChangeTime);
        }

        ActivateGameOverMenu();
    }

    private void ActivateGameOverMenu()
    {
        _txtGameOverText.gameObject.SetActive(true);
        _btnRestart.SetActive(true);
        _btnQuit.SetActive(true);
    }
}
