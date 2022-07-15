using System;
using UnityEngine;

[Serializable]
public class BlobSettings
{

    [SerializeField] private float minblobScale;

    [SerializeField] private float maxBlobScale;

    [SerializeField] private float blobSpeed;

    [SerializeField] private float blobDelayTime;

    [SerializeField] private float minBlobBackgroundScale;

    [SerializeField] private float maxBlobBackgroundScale;

    [SerializeField] private int minBlobBackground;

    [SerializeField] private int maxBlobBackground;

    [SerializeField] private float blobBackgroundDelayTime;

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
