using System;
using UnityEngine;

[Serializable]
public class HalvesPhysicsSettings
{
    [SerializeField] private float minImpuls;
    [SerializeField] private float maxImpuls;
    [SerializeField][Min(0)] private float minSpeedDown;
    [SerializeField][Min(0)] private float maxSpeedDown;

    public float MinImpuls => minImpuls;
    public float MaxImpuls => maxImpuls;

    public float MinSpeedDown => minSpeedDown;
    public float MaxSpeedDown => maxSpeedDown;
}
