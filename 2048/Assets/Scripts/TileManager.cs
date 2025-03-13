using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[System.Serializable]
public class TileProperties
{
    [SerializeField] private int value;
    [SerializeField] private Color tileColor;
    [SerializeField] private Color textColor;

    public int Value => value;

    public Color TileColor => tileColor;

    public Color TextColor => textColor;
}

public class TileManager : MonoBehaviour
{
    [SerializeField] private TileProperties[] tileProperties;
    [SerializeField] private GameObject tilePrefab;
    private GameObject[,] _cells;
    private string _direction;
    private const int Rows = 4; // Set this based on your grid size
    private const int Cols = 4; // Set this based on your grid size
    private Tile[,] _tiles;

    void Start()
    {
        var foundCells = GameObject.FindGameObjectsWithTag("Cell");

        // Sort the array by the order of the objects in the hierarchy
        foundCells = foundCells.OrderBy(cell => cell.transform.GetSiblingIndex()).ToArray();

        if (foundCells.Length != Rows * Cols)
        {
            Debug.LogError("Grid size mismatch! Check the number of cells.");
            return;
        }

        _cells = new GameObject[Cols, Rows];
        _tiles = new Tile[Cols, Rows];

        for (var col = 0; col < Cols; col++)
        {
            for (var row = 0; row < Rows; row++)
            {
                _cells[col, row] = foundCells[row * Cols + col]; // Mapping adjusted
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTile();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = "left";
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = "right";
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = "up";
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = "down";
        }
    }

    private void SpawnTile()
    {
        bool hasEmptyCell = false;
        foreach (var cell in _cells)
        {
            if (cell.transform.childCount == 0)
            {
                hasEmptyCell = true;
                break;
            }
        }

        if (!hasEmptyCell)
        {
            Debug.Log("Game over!");
            return;
        }

        int col, row;
        do
        {
            col = Random.Range(0, _cells.GetLength(0)); // Get random col
            row = Random.Range(0, _cells.GetLength(1)); // Get random row
        } while (_cells[col, row].transform.childCount != 0);

        if (tilePrefab == null) return;
        var newTile = Instantiate(tilePrefab, _cells[col, row].transform);
        _tiles[col, row] = newTile.gameObject.GetComponent<Tile>();
        Debug.Log(col + ", " + row);
    }

    private void MoveTile(string direction)
    {
        // left: cols: 0 1 2 3
        // right: cols: 3 2 1 0
        // up: rows: 0 1 2 3 
        // down: rows: 3 2 1 0 
    }

    private void ShiftLeft()
    {
        for (var col = 0; col < Cols; col++)
        {
            for (var row = 0; row < Rows; row++)
            {
                // Adjust logic here based on new column-major order
            }
        }
    }

    public TileProperties[] TileProperties
    {
        get => tileProperties;
        set => tileProperties = value;
    }
}