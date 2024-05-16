using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform _player;
    void Update()
    {
        _cam.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _cam.transform.position.z);
    }
}
