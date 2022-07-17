using System;
using UnityEngine;

[Serializable]
public class MagnetSettings
{
    [SerializeField] private Sprite blobSprite;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private float attractionTime;

    [SerializeField][Min(0)] private float forceAttraction;

    [SerializeField][Min(0)] private float maxBlobSize;

    [SerializeField][Min(0)] private float minBlobSize;

    [SerializeField][Min(0)] private float timeLiveBlob;

    public float TimeLiveBlob => timeLiveBlob;

    public Sprite BlobSprite => blobSprite;

    public float RadiusCollider => radiusCollider;
    public float AttractionTime => attractionTime;

    public float MaxBlobSize => maxBlobSize;
    public float MinBlobSize => minBlobSize;

    public float ForceAttractionTime => forceAttraction;

}
