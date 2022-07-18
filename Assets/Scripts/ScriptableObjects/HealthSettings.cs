using System;
using UnityEngine;

[Serializable]
public class HealthSettings
{
    [SerializeField] private Sprite blobSprite;

    [SerializeField] private Sprite heartSpriteOnPanel;

    [SerializeField][Min(0)] private float maxBlobSize;

    [SerializeField][Min(0)] private float minBlobSize;

    [SerializeField][Min(0)] private int startHealth;

    [SerializeField][Min(0)] private int maxHealth;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private float timeMoveHeartToHeartPanel;

    [SerializeField][Min(0)] private int countHealthAdd;

    [SerializeField][Min(0)] private float timeBeat;

    [SerializeField][Min(0)] private float minHeartBeatScale;
    [SerializeField][Min(0)] private float maxHeartBeatScale;

    [SerializeField][Min(0)] private float heartBeatSpeed;

    [SerializeField] private float increaseImpuls;

    public Sprite BlobSprite => blobSprite;

    public Sprite HeartSpriteOnPanel => heartSpriteOnPanel;

    public float MaxBlobSize => maxBlobSize;
    public float MinBlobSize => minBlobSize;

    public float TimeBeat => timeBeat;

    public int StartHealth => startHealth;

    public int MaxHealth => maxHealth;

    public float RadiusCollider => radiusCollider;

    public float TimeMoveHeartToHeartPanel => timeMoveHeartToHeartPanel;
    
    public int CountHealthAdd => countHealthAdd;


    public float MinHeartBeatScale => minHeartBeatScale;
    public float MaxHeartBeatScale => maxHeartBeatScale;

    public float HeartBeatSpeed => heartBeatSpeed;

    public float IncreaseImpuls => increaseImpuls;
}
