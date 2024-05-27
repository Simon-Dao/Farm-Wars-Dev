using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InputManager : MonoBehaviour, IPunObservable
{
    public bool _ePressed = false;
    public bool _plant_active = false;

    private PhotonView photonView;

    public void DestroyNetworkObject(string objToDestroy)
    {   
        photonView.RPC("DestroyObj", RpcTarget.AllBuffered, objToDestroy);
    }

    [PunRPC]
    public void DestroyObj(string objToDestroy) {
        Destroy(GameObject.Find(objToDestroy));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_plant_active);
            stream.SendNext(_ePressed);
        }
        else if (stream.IsReading)
        {
            _plant_active = (bool)stream.ReceiveNext();
            _ePressed = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void SetPlant(bool newState)
    {
        Debug.Log("setting plant " + newState);
        _plant_active = newState;
    }
    public void CallPlantChange(bool newState)
    {
        if (newState != _plant_active)
        {
            photonView.RPC("SetPlant", RpcTarget.AllBuffered, newState);
        }
    }

    [PunRPC]
    void SetEPressed(bool newState)
    {
        _ePressed = newState;
    }
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) != _ePressed)
        {
            photonView.RPC("SetEPressed", RpcTarget.AllBuffered, Input.GetKey(KeyCode.E));
        }
    }
}
