using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Grid _grid;
    [SerializeField] private Tile _forestTile, _waterTile, _landTile;
    [SerializeField] private Transform _cam;
    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var randomTile = UnityEngine.Random.Range(0, 5) < 3 ? _forestTile : _landTile;
                if (randomTile == _forestTile) randomTile = UnityEngine.Random.Range(0, 5) == 4 ? _waterTile : _forestTile;
                var worldPosition = _grid.GetCellCenterWorld(new Vector3Int(x, y));
                var spawnedTile = Instantiate(randomTile, worldPosition, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = UnityEngine.Random.Range(0, 2) == 1 ? true : false;
                spawnedTile.Init(isOffset);


                _tiles[new Vector2(x, y)] = spawnedTile;
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