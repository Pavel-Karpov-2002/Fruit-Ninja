using System;
using UnityEngine;

[Serializable]
public class FruitSprite
{
    [SerializeField] private Sprite fruitsSprite;
    [SerializeField] private Sprite blobSprite;
    [SerializeField] private Color colorPoints;

    public Sprite FruitsSprite => fruitsSprite;

    public Sprite BlobSprite => blobSprite;

    public Color ColorFruit => colorPoints;
}
