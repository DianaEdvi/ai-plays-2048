using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private Color tileColor;
    [SerializeField] private Color textColor;

    private void Start()
    {
        value = int.Parse(gameObject.GetComponentInChildren<TMP_Text>().text);
        tileColor = gameObject.GetComponent<UnityEngine.UI.Image>().color;
        textColor = gameObject.GetComponentInChildren<TMP_Text>().color;
    }
}

