using System;
using UnityEngine;

[Serializable]
public class BombSettigns
{
    [SerializeField] private float explosionRadius;

    [SerializeField] private float centerExplosionImpuls;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private int damage;

    [SerializeField][Min(0)] private int maxChanceConvertBombIntoFruits;

    [SerializeField][Min(0)] private float timeBeforeExplosion;

    [SerializeField] private GameObject explosionSprites;

    [SerializeField] private float maxScaleExplosion;

    public int MaxChanceConvertBombIntoFruits => maxChanceConvertBombIntoFruits;

    public float ExplosionRadius => explosionRadius;

    public float CenterExplosionImpuls => centerExplosionImpuls;

    public float RadiusCollider => radiusCollider;

    public int Damage => damage;

    public float TimeBeforeExplosion => timeBeforeExplosion;

    public GameObject ExplosionSprites => explosionSprites;

    public float MaxScaleExplosion => maxScaleExplosion;
}
