using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ForestTile : Tile
{
    [SerializeField] private GameObject _self;
    [SerializeField] private Tile _land;
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

        if (_touch && Bank.money >= cost && _ePressed)
        {
            Bank.money -= cost;
            var worldPosition = _self.transform.position;
            Destroy(_self);
            var spawnedTile = Instantiate(_land, worldPosition, Quaternion.identity);
            spawnedTile.name = $"Tile {worldPosition.x} {worldPosition.y}";

            var isOffset = UnityEngine.Random.Range(0, 2) == 1 ? true : false;
            spawnedTile.Init(isOffset);
        }
    }
}
