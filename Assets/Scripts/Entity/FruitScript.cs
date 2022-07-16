using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class FruitScript : Entity
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private float speedRotate = 1.5f;

    private Transform scaleShadow;
    private int numFruit;
    private FruitSettings fruitSettings;

    public FruitSettings FruitSettings => fruitSettings;

    private void Awake()
    {
        CreateShadowScript.CreateShadow(gameObject);
        scaleShadow = gameObject.GetComponentInChildren<CreateShadowScript>().transform;

        player = FindObjectOfType<GamePlayEvents>();
        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script");

        numFruit = Random.Range(0, settings.FruitSettings.Count);
        fruitSettings = settings.FruitSettings[numFruit];
        gameObject.GetComponent<SpriteRenderer>().sprite = fruitSettings.FruitsSprite;
    }

    private void Start()
    {
        player.Entitys.Add(this);

        transform.DORotate(new Vector3(0, 0, 180), speedRotate).SetLoops(-180, LoopType.Incremental).SetEase(Ease.Linear);

        NormalizeObject();
    }

    private void FixedUpdate()
    {
        NormalizeObject();
    }

    private void NormalizeObject()
    {
        ScaleChangeScript.ChangeOnWindow(transform);

        scaleShadow.transform.localScale += new Vector3(0.3f, 0.3f);
        ScaleChangeScript.ChangeOnWindow(scaleShadow.transform);

        ChageRadius();
    }

    private void ChageRadius()
    {
        if (fruitSettings.RadiusCollider == 0)
            RadiusCollider = (transform.localScale.y / GetComponent<SpriteRenderer>().bounds.size.y) * 2f;
        else
            RadiusCollider = fruitSettings.RadiusCollider;
    }

    public override void Destruction()
    {
        player.AddPoint(settings.NumberOfPointsPerFruit, gameObject, fruitSettings.ColorFruit);
        CutSpriteScript.GetTwoHalves(gameObject.GetComponent<SpriteRenderer>().sprite.texture, gameObject);

        BlobSettings blobSetting = settings.BlobSettings;

        CreateBlobScript.CreateMoreBlub(gameObject,
            fruitSettings.BlobSprite,
            settings);

        CreateBlobScript.CreateOneBlob(
            gameObject,
            fruitSettings.BlobSprite,
            blobSetting.MinBlobScale,
            blobSetting.MaxBlobScale,
            blobSetting.BlobSpeed,
            blobSetting.BlobDelayTime,
            blobSetting.LayerBlob);

        player.Entitys.Remove(this);

        Destroy(gameObject);
    } 

    private void OnBecameInvisible()
    {
        float halfHeight = WorldSizeCamera.HalfHeight;

        if (gameObject.activeSelf && transform.position.y < halfHeight)
        {
            if (player != null)
                player.SubstractHealth(1);

            Destroy(gameObject);
        }
    }
}
