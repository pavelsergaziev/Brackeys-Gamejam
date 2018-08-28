using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Collider2D))]
public class Trigger : MonoBehaviour {

    public string meta;
    [HideInInspector]
    public bool isTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerScript = collision.GetComponent<PlayerController>();
        if (playerScript!=null)
        {
            isTriggered = true;
        }
    }
}
