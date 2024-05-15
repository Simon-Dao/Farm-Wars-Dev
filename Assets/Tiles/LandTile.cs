using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandTile : Tile
{
    [SerializeField] private GameObject _plant;
    void Update()
    {
        if (_touch && Bank.money >= cost && Input.GetKey(KeyCode.E) && !_plant.activeSelf)
        {
            Bank.money -= cost;
            _plant.SetActive(true);
        }
    }
}
