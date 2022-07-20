using System;
using UnityEngine;

[Serializable]
public class FreezingSettings
{
    [SerializeField] private Sprite blobSprite;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private float timeReduction;

    [SerializeField][Min(0)] private float speedReduction;

    public Sprite BlobSprite => blobSprite;

    public float RadiusCollider => radiusCollider;

    public float TimeReduction => timeReduction;

    public float SpeedReduction => speedReduction;
}
