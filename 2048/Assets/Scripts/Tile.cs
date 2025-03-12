using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color tileColor;
    [SerializeField] private Color textColor;
    [SerializeField] private TMP_Text text;
    private TileProperties[] _tileClassifiers;
    private TileManager _tileManager;

    public TMP_Text Text
    {
        get => text;
        set => text = value;
    }

    public Color TileColor
    {
        get => tileColor;
        set => tileColor = value;
    }

    public Color TextColor
    {
        get => textColor;
        set => textColor = value;
    }

    private void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        tileColor = gameObject.GetComponent<Image>().color;
        textColor = gameObject.GetComponentInChildren<TMP_Text>().color;
        _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();

        if (_tileManager != null)
        {
            _tileClassifiers = _tileManager.TileClassifiers;
        }

        StartingTile();
    }

    /**
     * Randomize the starting tile to be either a 2 or a 4
     */
    private void StartingTile()
    {
        var chanceOf2 = Random.Range(0, 3);

        if (chanceOf2 != 0) return;

        text.text = _tileClassifiers[1].Value + "";
        textColor = _tileClassifiers[1].TextColor;
        tileColor = _tileClassifiers[1].TileColor;
    }
}