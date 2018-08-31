using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof (Collider2D))]
public class Trigger : MonoBehaviour {

    public string meta;
    public bool destroyAfterCollision;
    public bool reTriger;
    public bool sentToEventManager;
    [HideInInspector]
    public bool isTriggered = false;

    private EventManager _eventManager;
    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerScript = collision.GetComponent<PlayerController>();
        if (playerScript!=null)
        {
            if (sentToEventManager)
            {
                _eventManager.StartEvent(meta);
            }
            StartCoroutine(Triggered(reTriger));
            if (destroyAfterCollision)
            {
                Destroy(gameObject, 0.2f);
            }
        }
    }
    IEnumerator Triggered(bool reTriger)
    {
        isTriggered = true;
        yield return new WaitForSeconds(0.1f);
        if (reTriger)
        {
            isTriggered = false;
        }
        

    }
}
