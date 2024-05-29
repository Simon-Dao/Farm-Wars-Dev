using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ForestTile : Tile
{
    [SerializeField] private GameObject _self;
    [SerializeField] private Tile _land;

    private InputManager inputManager;

    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        gameObject.name = _tile_id;
    }

    GameObject FindGameObjectWithID(string nameToFind)
    {
        // Find all game objects in the scene
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (Tile t in tiles)
        {
            if (t._tile_id == nameToFind)
            {
                return t.gameObject;
            }
        }
        return null; // Return null if no matching animal is found
    }

    void Update()
    {

        if (!_touch) return;
        if(_currPlayer == null) return;

        updateEPressed(inputManager);

        if (Bank.money >= cost && inputManager.IsTileTriggered(_tile_id, _currPlayer._playerID))
        {
            Debug.Log(_tile_id);
            Bank.money -= cost;
            var worldPosition = _self.transform.position;
            inputManager.DestroyNetworkObject(_self.name);
            var spawnedTile = PhotonNetwork.Instantiate(_land.name, worldPosition, Quaternion.identity);
            spawnedTile.name = _tile_id.Replace("forest", "land");

            var isOffset = UnityEngine.Random.Range(0, 2) == 1 ? true : false;
            spawnedTile.GetComponent<Tile>().Init(isOffset);
        }
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
    void SetTileID(string name)
    {
        _tile_id = name;
        gameObject.name = name;
    }

    public void CallTileIDSet(string name)
    {
        photonView.RPC("SetTileID", RpcTarget.AllBuffered, name);
    }
}
