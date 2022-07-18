using TMPro;
using UnityEngine;
using System;

[Serializable]
public class TextMeshProSettings
{

    [SerializeField] private GameObject textPointsStile;

    [SerializeField] private Material textPointsMaterial;

    public GameObject TextPointsStyle => textPointsStile;

    public Material TextPointsMaterial => textPointsMaterial;
}
