using UnityEngine;
using System;

[Serializable]
public class FruitBagSettings
{
    [SerializeField][Min(0)] private int minAngleImpulseFruit;

    [SerializeField][Min(0)] private int maxAngleImpulseFruit;

    [SerializeField][Min(0)] private float minImpulsFruit;

    [SerializeField][Min(0)] private float maxImpulsFruit;

    [SerializeField][Min(0)] private int minFruitsInBag;

    [SerializeField][Min(0)] private int maxFruitsInBag;

    [SerializeField][Min(0)] private float radiusCollider;

    [SerializeField] private int maxChanceConvertHeartIntoFruits;

    public int MinAngleImpulseFruit => minAngleImpulseFruit;
    public int MaxAngleImpulseFruit => maxAngleImpulseFruit;

    public float MinImpulsFruit => minImpulsFruit;
    public float MaxImpulsFruit => maxImpulsFruit;

    public int MinFruitsInBag => minFruitsInBag;
    public int MaxFruitsInBag => maxFruitsInBag;

    public float RadiusCollider => radiusCollider;

    public int MaxChanceConvertHeartIntoFruits => maxChanceConvertHeartIntoFruits;
}
