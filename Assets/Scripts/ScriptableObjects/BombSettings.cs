using System;
using UnityEngine;

[Serializable]
public class BombSettigns
{
    [SerializeField][Min(0)] private float explosionRadius;

    [SerializeField] private float centerExplosionImpuls;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField][Min(0)] private int damage;

    [SerializeField][Min(0)] private float timeExplosion;

    [SerializeField][Min(0)] private float maxScaleExplosion;


    public float ExplosionRadius => explosionRadius;

    public float CenterExplosionImpuls => centerExplosionImpuls;

    public float RadiusCollider => radiusCollider;

    public int Damage => damage;

    public float TimeExplosion => timeExplosion;

    public float MaxScaleExplosion => maxScaleExplosion;
}
