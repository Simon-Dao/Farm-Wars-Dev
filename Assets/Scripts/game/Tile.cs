using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class Tile : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _plant;
    [SerializeField] private int cost;
    [SerializeField] private bool _touch;

    public bool _ePressed;
    public bool _plant_active;
    public void Start() {
        _plant_active = false;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        if(stream.IsWriting) {
            stream.SendNext(_plant_active);
            stream.SendNext(_ePressed);
        } else if(stream.IsReading) {
            _plant_active = (bool)stream.ReceiveNext();
            _ePressed = (bool)stream.ReceiveNext();
        }
    } 
    [PunRPC]
    void SetPlant(bool newState) 
    {
        _plant_active = newState;
    }
    [PunRPC]
    void SetEPressed(bool newState) 
    {
        _ePressed = newState;
    }

    // Example method to call RPC to increase player score
    public void CallPlantChange(bool newState) 
    {
        photonView.RPC("SetPlant", RpcTarget.AllBuffered, newState);
    }

    public void CallEPressed(bool newState) 
    {
        if(newState != _ePressed)
        photonView.RPC("SetEPressed", RpcTarget.AllBuffered, newState);
    }
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
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
    void Update()
    {
        CallEPressed(Input.GetKey(KeyCode.E));

        if (_ePressed && _touch)
        {
            if (Bank.money >= cost && !_plant.activeSelf)
            {
                Bank.money -= cost;
                CallPlantChange(true);
            }
        }

        _plant.SetActive(_plant_active);
    }
}