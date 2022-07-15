using System;
using UnityEngine;

[Serializable]
public class FruitSprite
{
    [SerializeField] private Sprite fruitsSprite;
    [SerializeField] private Sprite blobSprite;

    public Sprite FruitsSprite => fruitsSprite;

    public Sprite BlobSprite => blobSprite;
}
