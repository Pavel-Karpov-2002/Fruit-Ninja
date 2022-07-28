using System;
using UnityEngine;

[Serializable]
public class ScaleOnWindowSettings
{
    [SerializeField][Min(0)] private float minScaleOnWindow;

    public float MinScaleOnWindow => minScaleOnWindow;
}
