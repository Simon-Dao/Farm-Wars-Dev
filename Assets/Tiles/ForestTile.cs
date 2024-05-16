using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTile : Tile
{
    [SerializeField] private GameObject _self;
    [SerializeField] private Tile _land;
    void Update()
    {
        if (_touch && Bank.money >= cost && Input.GetKey(KeyCode.E))
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
