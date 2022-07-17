using System;
using UnityEngine;

[Serializable]
public class ScaleOnWindowSettings
{
    [SerializeField] private float minScaleOnWindow;
    [SerializeField] private float maxScaleOnWindow;

    public float MinScaleOnWindow => minScaleOnWindow;
    public float MaxScaleOnWindow => maxScaleOnWindow;
}
