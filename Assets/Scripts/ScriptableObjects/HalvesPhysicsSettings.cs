using System;
using UnityEngine;

[Serializable]
public class HalvesPhysicsSettings
{
    [SerializeField] private float minImpuls;
    [SerializeField] private float maxImpuls;
    [SerializeField] private int minAngle;
    [SerializeField] private int maxAngle;
    [SerializeField] private float speed;
    [SerializeField] private float timeLive;

    public float MinImpuls => minImpuls;
    public float MaxImpuls => maxImpuls;

    public int MinAngle => minAngle;
    public int MaxAngle => maxAngle;

    public float Speed => speed;

    public float TimeLive => timeLive;
}
