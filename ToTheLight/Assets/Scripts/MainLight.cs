using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLight : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerScript = other.GetComponent<PlayerController>();
        if (playerScript != null)
        {
            FindObjectOfType<GameStateManager>().GameOverVictory();
        }
    }

}
