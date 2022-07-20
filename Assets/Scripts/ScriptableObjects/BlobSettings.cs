using System;
using UnityEngine;

[Serializable]
public class BlobSettings
{

    [SerializeField][Min(0)] private float minBlobScale;

    [SerializeField][Min(0)] private float maxBlobScale;

    [SerializeField][Min(0)] private int minBlobCount;

    [SerializeField][Min(0)] private int maxBlobCount;

    [SerializeField] private float minBlobSpeedDisappearance;

    [SerializeField] private float maxBlobSpeedDisappearance;

    [SerializeField] private float layerBlob;


    public float MinBlobScale => minBlobScale;
    public float MaxBlobScale => maxBlobScale;

    public int MinBlobCount => minBlobCount;
    public int MaxBlobCount => maxBlobCount;

    public float MinBlobSpeedDisappearance => minBlobSpeedDisappearance;
    public float MaxBlobSpeedDisappearance => maxBlobSpeedDisappearance;

    public float LayerBlob => layerBlob;
}
