using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private float _camSpeed = 1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _cam.Translate(new Vector3(0, _camSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _cam.Translate(new Vector3(-_camSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _cam.Translate(new Vector3(0, -_camSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _cam.Translate(new Vector3(_camSpeed * Time.deltaTime, 0, 0));
        }
    }
}
