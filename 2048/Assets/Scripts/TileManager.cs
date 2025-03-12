using System;
using System.Collections;
using System.Collections.Generic;
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
    private GameObject[] _cells;


    // Start is called before the first frame update
    void Start()
    {
        _cells = GameObject.FindGameObjectsWithTag("Cell");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnTile();
        }
    }

    /**
     * Spawns a new tile in a random open spot on the board
     */
    private void SpawnTile()
    {
        var randomCell = Random.Range(0, _cells.Length);

        // Check if there are still empty cells available
        if (Array.Exists(_cells, cell => cell.transform.childCount == 0))
        {
            // If you find a cell that's already filled, try again 
            while (_cells[randomCell].transform.childCount != 0)
            {
                randomCell = Random.Range(0, _cells.Length);
            }

            // Spawn the tile 
            if (tilePrefab == null) return;
            Instantiate(tilePrefab, _cells[randomCell].transform);
        }
        else
        {
            Debug.Log("Game over!");
        }
    }

    public TileProperties[] TileClassifiers
    {
        get => tileProperties;
        set => tileProperties = value;
    }
}