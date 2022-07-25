using System;
using UnityEngine;

[Serializable]
public class MagnetSettings
{
    [SerializeField] private Sprite blobSprite;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private float attractionTime;

    [SerializeField][Min(0)] private float speedAttraction;

    [SerializeField][Min(0)] private float radiusAttraction;

    [SerializeField][Min(0)] private float minImpulsDown;

    [SerializeField][Min(0)] private float maxImpulsDown;

    [SerializeField][Min(0)] private float minAngleDown;
    [SerializeField][Min(0)] private float maxAngleDown;

    public Sprite BlobSprite => blobSprite;

    public float RadiusCollider => radiusCollider;
    public float AttractionTime => attractionTime;

    public float SpeedAttraction => speedAttraction;

    public float RadiusAttraction => radiusAttraction;

    public float MinImpulsDown => minImpulsDown;
    public float MaxImpulsDown => maxImpulsDown;

    public float MinAngleDown => minAngleDown;
    public float MaxAngleDown => maxAngleDown;
}
