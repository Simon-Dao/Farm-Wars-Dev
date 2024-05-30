using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ForestTile : Tile
{
    [SerializeField] private GameObject _self;
    [SerializeField] private Tile _land;

    private Bank bank;
    private InputManager inputManager;

    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        bank = GameObject.Find("Controller").GetComponent<Bank>();
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

        if (bank.GetBalance(_currPlayer._playerID) >= cost && inputManager.IsTileTriggered(_tile_id, _currPlayer._playerID))
        {
            // bank._accounts[_currPlayer._playerID] -= cost;
            bank.CallDeposit(_currPlayer._playerID, -cost); 
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
