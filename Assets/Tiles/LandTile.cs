using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LandTile : Tile
{
    [SerializeField] private GameObject _plant;
    
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

    void Update()
    {
        CallEPressed(Input.GetKey(KeyCode.E));

        if (_touch && Bank.money >= cost && _ePressed && !_plant.activeSelf)
        {
            Bank.money -= cost;
            // _plant.SetActive(true);
            CallPlantChange(true);
        }

        _plant.SetActive(_plant_active);
    }
}
