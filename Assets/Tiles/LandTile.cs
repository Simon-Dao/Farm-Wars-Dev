using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LandTile : Tile
{
    [SerializeField] private GameObject _plant;
    [SerializeField] public SpriteRenderer _c;
    [SerializeField] protected Owner _owner;
    [SerializeField] protected Player _currPlayer = null;
    public float _multiplier = 1f;
    public float _multiplierMultiplier = 1.1f;
    public float _plantCost = 50f;

    private InputManager inputManager;

    void Start() {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _tile_name = "land";
    }

    void Update()
    {
        if (!_touch) return;

        if (inputManager._ePressed)
        {
            if (Bank.money >= cost && !inputManager._plant_active && _owner == null)
            {
                PurchaseTile();
            }
            else if (Bank.money >= _plantCost && !inputManager._plant_active && _owner == _currPlayer)
            {
                PlantOnTile();
            }
            else if (Bank.money >= (cost * _multiplier) && !inputManager._plant_active && _owner != _currPlayer && _owner != null)
            {
                PurchaseTileFromOtherPlayer();
            }
        }
        _plant.SetActive(inputManager._plant_active);
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
        inputManager.CallPlantChange(true);
        // _plant.GetComponent<Plant>().Reset();
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
}