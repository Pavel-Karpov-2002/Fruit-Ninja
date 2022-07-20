using System;
using UnityEngine;

[Serializable]
public class ScaleOnWindowSettings
{
    [SerializeField][Min(0)] private float maxScaleOnWindow;

    public float MaxScaleOnWindow => maxScaleOnWindow;
}
