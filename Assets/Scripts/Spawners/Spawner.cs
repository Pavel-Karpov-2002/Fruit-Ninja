using System;
using UnityEngine;

[Serializable]
public class Spawner
{

    [SerializeField][Range(0, 100)] private float bottomStartPosition;
    [SerializeField][Range(0, 100)] private float bottomEndPosition;

    [SerializeField][Range(0, 100)] private float heightStartPosition;
    [SerializeField][Range(0, 100)] private float heightEndPosition;

    [SerializeField][Range(0, 360)] private float minAngle;
    [SerializeField][Range(0, 360)] private float maxAngle;

    [SerializeField][Min(0)] private int minObjects;
    [SerializeField][Min(0)] private int maxObjects;

    [SerializeField][Min(0)] private float minImpuls;
    [SerializeField][Min(0)] private float maxImpuls;

    [SerializeField][Min(0)] private float maxProcentCountBombInPull;

    [SerializeField][Min(0)] private int priority;

    [SerializeField] private bool isLeft;


    public float MaxProcentCountBombInPull => maxProcentCountBombInPull;

    public float BottomStartPosition => bottomStartPosition;
    public float BottomEndPosition => bottomEndPosition;

    public float HeightStartPosition => heightStartPosition;
    public float HeightEndPosition => heightEndPosition;

    public float MinAngle => minAngle;
    public float MaxAngle => maxAngle;

    public int MinObjects => minObjects;
    public int MaxObjects => maxObjects;

    public float MinImpuls => minImpuls;
    public float MaxImpuls => maxImpuls;

    public int Priority => priority;

    public bool IsLeft => isLeft;
}
