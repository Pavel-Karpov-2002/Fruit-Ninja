using System;
using UnityEngine;

[Serializable]
public class HealthSettings
{
    [SerializeField] private Sprite blobSprite;

    [SerializeField] private Sprite heartSpriteOnPanel;

    [SerializeField] private float maxBlobSize;

    [SerializeField] private float minBlobSize;

    [SerializeField][Min(0)] private int startHealth;

    [SerializeField][Min(0)] private int maxHealth;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private float timeMoveHeartToHeartPanel;

    [SerializeField][Min(0)] private int countHealthAdd;

    [SerializeField][Min(0)] private float timeBeat;

    [SerializeField] private float minHeartBeatScale;
    [SerializeField] private float maxHeartBeatScale;

    [SerializeField] private float heartBeatSpeed;

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
}
