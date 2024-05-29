using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Data.Common;
using TMPro;

public class LandTile : Tile
{
    [SerializeField] private GameObject _plant;
    [SerializeField] public SpriteRenderer _c;
    [SerializeField] protected Owner _owner;
    public float _multiplier = 1f;
    public float _multiplierMultiplier = 1.1f;
    public float _plantCost = 50f;

    private string _animation_state = "";
    private InputManager inputManager;

    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        gameObject.name = _tile_id;
    }

    void Update()
    {
        if (!_touch) return;
        if(_currPlayer == null) return;

        updateEPressed(inputManager);

        if (inputManager.IsTileTriggered(_tile_id, _currPlayer._playerID))
        {
            if (Bank.money >= cost && !_plant.activeSelf && _owner == null)
            {
                PurchaseTile();
            }
            else if (Bank.money >= _plantCost && !_plant.activeSelf && _owner == _currPlayer)
            {
                PlantOnTile();
                ChangeAnimationState("Wheat");
            }
            else if (Bank.money >= (cost * _multiplier) && !_plant.activeSelf && _owner != _currPlayer && _owner != null)
            {
                PurchaseTileFromOtherPlayer();
            }
            inputManager.ResetActionTriggered();
        }
    }

    private void PurchaseTile()
    {
        Bank.money -= cost;
        _owner._playerID = _currPlayer._playerID;
        _owner._color = _currPlayer._color;
        _c.color = _owner._color;
    }

    private void PlantOnTile()
    {
        Bank.money -= _plantCost;
        _plant.SetActive(true);
        _multiplier *= _multiplierMultiplier;
    }

    private void PurchaseTileFromOtherPlayer()
    {
        Bank.money -= (cost * _multiplier);
        _owner._playerID = _currPlayer._playerID;
        _owner._color = _currPlayer._color;
        _c.color = _owner._color;
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {
        var player = gameObject.GetComponent<Player>();
        player._playerID = col.gameObject.GetComponent<Player>()._playerID;
        player._color = col.gameObject.GetComponent<Player>()._color;

        if (!_touch)
        {
            _touch = true;
            _currPlayer = player;
        }
        base.OnTriggerEnter2D(col);
    }

    private new void OnTriggerExit2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        _touch = false;
        _currPlayer = null;
        base.OnTriggerExit2D(col);
    }
    
    [PunRPC]
    void SetTileID(string name) {
        _tile_id = name;
        gameObject.name = name;
    }

    public void CallTileIDSet(string name) {
        photonView.RPC("SetTileID", RpcTarget.AllBuffered, name);
    }
    void ChangeAnimationState(string newState)
    {
        if (_animation_state == newState) return;
        _animation_state = newState;
    }
}