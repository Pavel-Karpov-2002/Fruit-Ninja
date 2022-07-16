using System;
using UnityEngine;

[Serializable]
public class FruitSettings
{
    [SerializeField] private Sprite fruitsSprite;

    [SerializeField] private Sprite blobSprite;

    [SerializeField] private Color colorPoints;

    [SerializeField][Min(0)] private float radiusCollider;

    public Sprite FruitsSprite => fruitsSprite;

    public Sprite BlobSprite => blobSprite;

    public Color ColorFruit => colorPoints;

    public float RadiusCollider => radiusCollider;
}
