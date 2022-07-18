using System;
using UnityEngine;

[Serializable]
public class FreezingSettings
{
    [SerializeField] private Sprite blobSprite;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private float timeScale;

    [SerializeField][Min(0)] private float timeReduction;

    [SerializeField][Min(0)] private float maxBlobSize;

    [SerializeField][Min(0)] private float minBlobSize;

    [SerializeField][Min(0)] private float timeLiveBlob;

    public Sprite BlobSprite => blobSprite;

    public float RadiusCollider => radiusCollider;

    public float TimeScale => timeScale;

    public float TimeReduction => timeReduction;

    public float MaxBlobSize => maxBlobSize;
    public float MinBlobSize => minBlobSize;

    public float TimeLiveBlob => timeLiveBlob;
}
