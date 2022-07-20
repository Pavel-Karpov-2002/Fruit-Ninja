using System;
using UnityEngine;

[Serializable]
public class ChancesOfSpawn
{
    [SerializeField] private Unit spawnObjectEntity;

    [SerializeField][Range(0, 100)] private float maxChanceOfSpawn;

    [SerializeField][Range(-1, 100)] private float maxPercentageInPool;

    [SerializeField][Min(0)] private int minPointsForSpawn;

    public Unit SpawnObjectEntity => spawnObjectEntity;

    public float MaxChanceOfSpawn => maxChanceOfSpawn;

    public float MaxPercentageInPool => maxPercentageInPool;

    public int MinPointsForSpawn => minPointsForSpawn;
}
