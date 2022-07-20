using System;
using TMPro;
using UnityEngine;

[Serializable]
public class TextMeshProSettings
{

    [SerializeField] private TextMeshPro textPointsStile;

    [SerializeField] private Material textPointsMaterial;

    [SerializeField][Min(0)] private float timeLive;

    public TextMeshPro TextPointsStyle => textPointsStile;

    public Material TextPointsMaterial => textPointsMaterial;

    public float TimeLive => timeLive;
}
