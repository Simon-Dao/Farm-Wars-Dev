using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Grid _grid;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        if(stream.IsWriting) {
            stream.SendNext(_tiles);

        } else if(stream.IsReading) {
            _tiles = (Dictionary<Vector2, Tile>)stream.ReceiveNext();
        }
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var worldPosition = _grid.GetCellCenterWorld(new Vector3Int(x, y));
                var spawnedTile = PhotonNetwork.Instantiate(_tilePrefab.name, worldPosition, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.GetComponent<Tile>().Init(isOffset);

                _tiles[new Vector2(x, y)] = spawnedTile.GetComponent<Tile>();
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}