using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class TileClassifier
{
    [SerializeField] private int value;
    [SerializeField] private Color tileColor;
    [SerializeField] private Color textColor;
}

public class TileProperties : MonoBehaviour
{
    [SerializeField] private TileClassifier[] tileClassifiers;
    [SerializeField] private GameObject[] cells;
    [SerializeField] private GameObject[] emptyCells; // Store childless cells
    [SerializeField] private GameObject tilePrefab;

    
    // Start is called before the first frame update
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Cell");
        emptyCells = new GameObject[cells.Length];
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
        var randomCell = Random.Range(0, cells.Length);

        // Check if there are still empty cells available
        if (Array.Exists(cells, cell => cell.transform.childCount == 0))
        {
            // If you find a cell that's already filled, try again 
            while (cells[randomCell].transform.childCount != 0)
            {
                randomCell = Random.Range(0, cells.Length);
            }

            // Spawn the tile 
            if (tilePrefab == null) return;
            Instantiate(tilePrefab, cells[randomCell].transform);
        }
        else
        {
            Debug.Log("Game over!");
        }
    }
}


