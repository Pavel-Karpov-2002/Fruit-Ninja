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

    public Sprite BlobSprite => blobSprite;

    public float RadiusCollider => radiusCollider;
    public float AttractionTime => attractionTime;

    public float SpeedAttraction => speedAttraction;

    public float RadiusAttraction => radiusAttraction;
}
