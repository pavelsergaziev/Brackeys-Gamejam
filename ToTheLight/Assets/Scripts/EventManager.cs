using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = SoundManager.instance;
        if (_soundManager ==null)
        {
            throw new System.Exception("EventManager не может найти ссылку на SoundManager");
        }
    }
    public void StartEvent(string eventMeta)
    {
        switch (eventMeta)
        {
            case "PlaySecondPartOfmainTheme":
                StartSecondMusicTheme();
                break;
            default:
                break;
        }
    }

    public void StartSecondMusicTheme()
    {
        _soundManager.PlaySecondPartOfmainTheme();
    }

    
}
