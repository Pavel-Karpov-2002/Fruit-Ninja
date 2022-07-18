using System;
using UnityEngine;

[Serializable]
public class ChancesOfSpawn
{
    [SerializeField] private GameObject spawnObject;

    [SerializeField][Range(0, 100)] private float maxChanceOfSpawn;

    [SerializeField][Range(-1, 100)] private float maxPercentageInPool;

    [SerializeField][Min(0)] private int minPointsForSpawn;

    public GameObject SpawnObject => spawnObject;

    public float MaxChanceOfSpawn => maxChanceOfSpawn;

    public float MaxPercentageInPool => maxPercentageInPool;

    public int MinPointsForSpawn => minPointsForSpawn;
}
