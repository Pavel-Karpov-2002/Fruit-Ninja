using System;
using UnityEngine;

[Serializable]
public class BlobSettings
{

    [SerializeField][Min(0)] private float minblobScale;

    [SerializeField][Min(0)] private float maxBlobScale;

    [SerializeField][Min(0)] private float blobSpeed;

    [SerializeField][Min(0)] private float blobDelayTime;

    [SerializeField][Min(0)] private float minBlobBackgroundScale;

    [SerializeField][Min(0)] private float maxBlobBackgroundScale;

    [SerializeField][Min(0)] private int minBlobBackground;

    [SerializeField][Min(0)] private int maxBlobBackground;

    [SerializeField][Min(0)] private float blobBackgroundDelayTime;

    [SerializeField] private float blobBackgroundSpeed;

    [SerializeField] private float layerBlobBackground;

    [SerializeField] private float layerBlob;

    public float MinBlobScale => minblobScale;

    public float MaxBlobScale => maxBlobScale;
    
    public float BlobSpeed => blobSpeed;

    public float BlobDelayTime => blobDelayTime;

    public float MinBlobBackgroundScale => minBlobBackgroundScale;
    public float MaxBlobBackgroundScale => maxBlobBackgroundScale;

    public int MinBlobBackground => minBlobBackground;

    public int MaxBlobBackground => maxBlobBackground;

    public float BlobBackgroundDelayTime => blobBackgroundDelayTime;

    public float BlobBackgroundSpeed => blobBackgroundSpeed;

    public float LayerBlobBackground => layerBlobBackground;

    public float LayerBlob => layerBlob;
}
