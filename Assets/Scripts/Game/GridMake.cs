using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class GridManager : MonoBehaviourPun
{
    [SerializeField] private int _width, _height;
    // [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _grid_prefab;

    [SerializeField] private Tile _forestTile, _waterTile, _landTile, _borderTile;
    [SerializeField] private Transform _cam;
    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GenerateGrid();
            PhotonNetwork.Instantiate(_grid_prefab.name, new Vector3(), Quaternion.identity);
        }
    }

    string GetRandomTileName(Tile randomTile)
    {
        string tileTitle = "";

        if (randomTile == _forestTile)
        {
            tileTitle = "forest";
        }
        else if (randomTile == _landTile)
        {
            tileTitle = "land";
        }
        else if (randomTile == _waterTile)
        {
            tileTitle = "water";
        }
        else
        {
            tileTitle = "tile";
        }
        return tileTitle;
    }
    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = -(_height / 2); x < _height / 2; x++)
        {
            for (int y = -(_width / 2); y < _width / 2; y++)
            {
                if (x == -(_height / 2) || y == -(_width / 2) || x == (_height / 2) - 1 || y == (_width / 2) - 1)
                {
                    var worldPosition = _grid_prefab.GetComponent<Grid>().GetCellCenterWorld(new Vector3Int(x, y));
                    var spawnedTile = PhotonNetwork.Instantiate(_borderTile.name, worldPosition, Quaternion.identity);
                    spawnedTile.name = $"Border {x} {y}";

                    _tiles[new Vector2(x, y)] = spawnedTile.GetComponent<Tile>();
                }
                else
                {
                    var randomTile = UnityEngine.Random.Range(0, 5) < 3 ? _forestTile : _landTile;
                    if (randomTile == _forestTile) randomTile = UnityEngine.Random.Range(0, 5) == 4 ? _waterTile : _forestTile;

                    string tileTitle = GetRandomTileName(randomTile);

                    var worldPosition = _grid_prefab.GetComponent<Grid>().GetCellCenterWorld(new Vector3Int(x, y));
                    var spawnedTile = PhotonNetwork.Instantiate(randomTile.name, worldPosition, Quaternion.identity);
                    spawnedTile.name = $"{tileTitle} {x} {y}";

                    // var isOffset = UnityEngine.Random.Range(0, 2) == 1 ? true : false;
                    spawnedTile.GetComponent<Tile>().Init(true);

                    _tiles[new Vector2(x, y)] = spawnedTile.GetComponent<Tile>();
                }
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