using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour {

    NotificationsManager _notificationsManager;

    [SerializeField]
    private string _text;
    [SerializeField]
    private float _time;

    private bool _isActivated;

    void Start()
    {
        _notificationsManager = FindObjectOfType<NotificationsManager>();
        _isActivated = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (!_isActivated)
        {
            _isActivated = true;
            var playerScript = other.GetComponent<PlayerController>();
            if (playerScript != null)
            {
                _notificationsManager.ShowNotification(_text, _time);
            }
        }

        Destroy(gameObject);
    }

}
