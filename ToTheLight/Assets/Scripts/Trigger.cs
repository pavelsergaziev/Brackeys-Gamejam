using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof (Collider2D))]
public class Trigger : MonoBehaviour {

    public string meta;
    public bool destroyAfterCollision;
    public bool reTriger;
    [HideInInspector]
    public bool isTriggered = false;
    public bool sentToEventManager;
    public bool OnTriggerEnter;
    public bool OnTriggerExit;

    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
        if (_eventManager==null)
        {
            throw new Exception("Trigger " + name + " не может найти ссылку на EventManager");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnTriggerEnter)
        {
            var playerScript = collision.GetComponent<PlayerController>();
            if (playerScript != null)
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
