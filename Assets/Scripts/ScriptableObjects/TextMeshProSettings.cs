using TMPro;
using UnityEngine;
using System;

[Serializable]
public class TextMeshProSettings
{

    [SerializeField] private GameObject textPointsStile;

    [SerializeField] private Material textPointsMaterial;

    [SerializeField] private float pointsLiveTime;

    public GameObject TextPointsStyle => textPointsStile;

    public Material TextPointsMaterial => textPointsMaterial;

    public float TextPointsLiveTime => pointsLiveTime;
}
