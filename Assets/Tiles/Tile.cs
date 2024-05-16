using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class Tile : MonoBehaviourPun, IPunObservable
{
    [SerializeField] protected Color _baseColor, _offsetColor;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected GameObject _highlight;
    [SerializeField] protected int cost;
    [SerializeField] protected bool _touch;

    public bool _ePressed;
    public bool _plant_active;
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


    // Example method to call RPC to increase player score
    public void CallPlantChange(bool newState)
    {
        photonView.RPC("SetPlant", RpcTarget.AllBuffered, newState);
    }

    public void CallEPressed(bool newState)
    {
        if (newState != _ePressed)
            photonView.RPC("SetEPressed", RpcTarget.AllBuffered, newState);
    }


    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _plant_active = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _highlight.SetActive(true);
            _touch = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _highlight.SetActive(false);
            _touch = false;
        }
    }
}