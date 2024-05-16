using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    private Transform _player;

    void Start()
    {
        _player = null;
    }

    void Update()
    {
        if (_player == null)
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                // Check if the object has a PhotonView component (indicating it's networked)
                if (obj.GetComponent<PhotonView>() != null && obj.GetComponent<PhotonView>().IsMine)
                {
                    // Add player objects to the list
                    _player = obj.transform;
                    break;
                }
            }
        } else {
            _cam.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _cam.transform.position.z);
        }
    }
}
