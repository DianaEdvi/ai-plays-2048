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


    // Start is called before the first frame update
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

        _cells = new GameObject[Rows, Cols];
        _tiles = new Tile[Rows, Cols];

        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Cols; j++)
            {
                _cells[i, j] = foundCells[i * Cols + j]; // Map from 1D to 2D
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTile();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = "left";
            Debug.Log("VAR");

            foreach (var tile in _tiles)
            {
                if (tile == null) return;
                ShiftDirections("left");
            }
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

    /**
     * Spawns a new tile in a random open spot on the board
     */
    private void SpawnTile()
    {
        // Check if there are still empty cells available
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

        int row, col;
        do
        {
            row = Random.Range(0, _cells.GetLength(0)); // Get random row
            col = Random.Range(0, _cells.GetLength(1)); // Get random column
        } while (_cells[row, col].transform.childCount != 0);

        // Spawn the tile 
        if (tilePrefab == null) return;
        var newTile = Instantiate(tilePrefab, _cells[row, col].transform);
        _tiles[row, col] = newTile.gameObject.GetComponent<Tile>();
    }


    private void MoveTile(string direction)
    {
        // left: rows: 0 1 2 3
        // right: rows: 3 2 1 0
        // up: cols: 0 1 2 3 
        // down: cols: 3 2 1 0 
    }

    private void ShiftDirections(string direction)
    {
        if (direction == "left")
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Cols; j++)
                {
                    if (j - 1 < 0) continue;
                    if (_tiles[i, j - 1] == null)
                    {
                        Debug.Log("move left one");
                    }
                }
            }
        }
    }

    public TileProperties[] TileClassifiers
    {
        get => tileProperties;
        set => tileProperties = value;
    }
}