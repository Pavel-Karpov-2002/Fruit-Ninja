using System;
using UnityEngine;

[Serializable]
public class FruitBagSettings
{
    [SerializeField][Range(0, 360)] private int minAngleImpulseFruit;

    [SerializeField][Range(0, 360)] private int maxAngleImpulseFruit;

    [SerializeField][Min(0)] private float minImpulsFruit;

    [SerializeField][Min(0)] private float maxImpulsFruit;

    [SerializeField][Min(0)] private int minFruitsInBag;

    [SerializeField][Min(0)] private int maxFruitsInBag;

    [SerializeField][Min(0)] private float radiusCollider;

    public int MinAngleImpulseFruit => minAngleImpulseFruit;
    public int MaxAngleImpulseFruit => maxAngleImpulseFruit;

    public float MinImpulsFruit => minImpulsFruit;
    public float MaxImpulsFruit => maxImpulsFruit;

    public int MinFruitsInBag => minFruitsInBag;
    public int MaxFruitsInBag => maxFruitsInBag;

    public float RadiusCollider => radiusCollider;
}
