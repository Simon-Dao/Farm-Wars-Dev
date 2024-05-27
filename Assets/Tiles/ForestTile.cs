using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ForestTile : Tile
{
    [SerializeField] private GameObject _self;
    [SerializeField] private Tile _land;
    // [PunRPC]
    // void SetPlant(bool newState)
    // {
    //     _plant_active = newState;
    // }
    private InputManager inputManager;

    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {

        if (_touch && Bank.money >= cost && inputManager._ePressed)
        {
            Bank.money -= cost;
            var worldPosition = _self.transform.position;
            Debug.Log(worldPosition);
            inputManager.DestroyNetworkObject(_self.name);
            var spawnedTile = PhotonNetwork.Instantiate(_land.name, worldPosition, Quaternion.identity);
            spawnedTile.name = //$"Tile {worldPosition.x} {worldPosition.y}";
            spawnedTile.name = gameObject.name;

            var isOffset = UnityEngine.Random.Range(0, 2) == 1 ? true : false;
            spawnedTile.GetComponent<Tile>().Init(isOffset);
        }
    }
}
