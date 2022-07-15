using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ScaleSettings
{
    [SerializeField] private float heartBeatScale;

    [SerializeField] private float heartBeatSpeed;

    public float HeartBeatScale => heartBeatScale;

    public float HeartBeatSpeed => heartBeatSpeed;

}
