using System;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LandTile : Tile
{
    [SerializeField] private GameObject _plant;
    [SerializeField] private SpriteRenderer _c;
    [SerializeField] private Owner _owner = null;
    private InputManager inputManager;
    private string _animation_state = "";
    public float _multiplier = 1f;
    public float _multiplierMultiplier = 1.1f;
    public float _plantCost = 50f;
    private Bank bank;

    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        bank = GameObject.Find("Controller").GetComponent<Bank>();
    }

    void Update()
    {
        if (!_touch || _currPlayer == null) return;

        updateEPressed(inputManager);

        if (inputManager.IsTileTriggered(_tile_id, _currPlayer._playerID))
        {
            ProcessTileInteractions();
            inputManager.ResetActionTriggered();
        }
    }

    private void ProcessTileInteractions()
    {   
        Debug.Log(_tile_id);
        if ((_owner == null || _owner._playerID == -1) && bank.GetBalance(_currPlayer._playerID) >= cost)
        {
            PurchaseTile();
        } else
        if (bank.GetBalance(_currPlayer._playerID) >= _plantCost && !_plant.activeSelf && _owner == _currPlayer)
        {
            PlantOnTile();
            ChangeAnimationState("Wheat");
        } else
        if (_owner != _currPlayer && bank.GetBalance(_currPlayer._playerID) >= (cost * _multiplier))
        {
            PurchaseTileFromOtherPlayer();
        }
    }

    private void PurchaseTile()
    {
//        bank._accounts[_currPlayer._playerID] -= cost;
        bank.CallDeposit(_currPlayer._playerID, -cost); 
        GetComponent<Owner>()._playerID = _currPlayer._playerID;

        GetComponent<Owner>()._playerName = _currPlayer._playerName;
        GetComponent<Owner>()._color = _currPlayer._color;
        transform.Find("ColorHold").GetComponent<SpriteRenderer>().color = _currPlayer._color;
    }

    private void PlantOnTile()
    {
        // bank._accounts[_currPlayer._playerID] -= _plantCost;
        bank.CallDeposit(_currPlayer._playerID, -_plantCost); 

        _plant.SetActive(true);
        _multiplier *= _multiplierMultiplier;
    }

    private void PurchaseTileFromOtherPlayer()
    {
        // bank._accounts[_currPlayer._playerID] -= (cost * _multiplier);
        bank.CallDeposit(_currPlayer._playerID, -(cost * _multiplier)); 
        GetComponent<Owner>()._playerID = _currPlayer._playerID;
        GetComponent<Owner>()._playerName = _currPlayer._playerName;
        GetComponent<Owner>()._color = _currPlayer._color;
        transform.Find("ColorHold").GetComponent<SpriteRenderer>().color = _currPlayer._color;
    }

    void ChangeAnimationState(string newState)
    {
        if (_animation_state == newState) return;
        _animation_state = newState;
    }

    [PunRPC]
    void SetTileID(string name)
    {
        _tile_id = name;
        gameObject.name = name;
    }

    public void CallTileIDSet(string name)
    {
        photonView.RPC("SetTileID", RpcTarget.AllBuffered, name);
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {

        if (!_touch)
        {
            var player = col.gameObject.GetComponent<Player>();
            player._playerID = col.gameObject.GetComponent<Player>()._playerID;
            player._color = col.gameObject.GetComponent<Player>()._color;

            this._currPlayer = player;

            GetComponent<Player>()._playerID = _currPlayer._playerID;
            GetComponent<Player>()._playerName = _currPlayer._playerName;
            GetComponent<Player>()._color = _currPlayer._color;
        }
        base.OnTriggerEnter2D(col);
    }

    private new void OnTriggerExit2D(Collider2D col)
    {
        _touch = false;
        _currPlayer = null;
        base.OnTriggerExit2D(col);
    }
}
