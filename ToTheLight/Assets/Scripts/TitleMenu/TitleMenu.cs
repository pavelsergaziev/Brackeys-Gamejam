using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _titlePanel;

    [SerializeField]
    private GameObject _creditsPanel;

    [SerializeField]
    private float _creditsRollingSpeed;

    private bool _inCredits;

    Vector3 _creditsPanelStartingPosition;

    void Update()
    {
        if (_inCredits)
        {
            if (Input.GetButtonDown("Pause") || Input.GetMouseButtonDown(0))
                ShowMainMenu();

            _creditsPanel.transform.position += new Vector3(0, Time.deltaTime * _creditsRollingSpeed, 0);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        _creditsPanelStartingPosition = _creditsPanel.transform.position;

        _titlePanel.SetActive(false);
        _creditsPanel.SetActive(true);
        _inCredits = true;
    }

    private void ShowMainMenu()
    {
        _creditsPanel.SetActive(false);
        _creditsPanel.transform.position = _creditsPanelStartingPosition;
        _titlePanel.SetActive(true);
        _inCredits = false;
    }



}
