using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Vector3 offset;
    public float smoothSpeed;

    private Transform _player;


    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }
    private void LateUpdate()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        Vector3 desiredPos = _player.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }
}
