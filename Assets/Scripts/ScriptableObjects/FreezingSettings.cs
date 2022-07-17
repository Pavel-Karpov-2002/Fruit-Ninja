using System;
using UnityEngine;

[Serializable]
public class FreezingSettings
{
    [SerializeField] private Sprite blobSprite;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Range(0, 100)] private int maxChanceConvertFreezingIntoFruits;

    [SerializeField] private float timeScale;

    [SerializeField] private float timeReduction;

    [SerializeField] private float maxBlobSize;

    [SerializeField] private float minBlobSize;

    [SerializeField] private float timeLiveBlob;

    public Sprite BlobSprite => blobSprite;

    public float RadiusCollider => radiusCollider;

    public int MaxChanceConvertFreezingIntoFruits => maxChanceConvertFreezingIntoFruits;

    public float TimeScale => timeScale;

    public float TimeReduction => timeReduction;

    public float MaxBlobSize => maxBlobSize;
    public float MinBlobSize => minBlobSize;

    public float TimeLiveBlob => timeLiveBlob;
}
