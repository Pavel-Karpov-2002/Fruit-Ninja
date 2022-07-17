using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class FruitScript : Entity
{
    [SerializeField] private GameSettings settings;

    private Transform scaleShadow;
    private int numFruit;
    private FruitSettings fruitSettings;
    private bool onVisible;
    private Vector3 start;

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
        start = transform.position;
        heightSprite = (GetComponent<SpriteRenderer>().sprite.bounds.size.y) / 2;
        player.Entitys.Add(this);

        transform.DORotate(new Vector3(0, 0, 180), settings.SpeedRotate).SetLoops(-180, LoopType.Incremental).SetEase(Ease.Linear);

        NormalizeObject();
    }

    private void FixedUpdate()
    {
        NormalizeObject();
        if (transform.position.y > 0 && transform.position.x > 0)
            onVisible = true;
    }

    private void NormalizeObject()
    {
        ScaleChangeScript.ChangeOnWindow(transform, settings.ScaleSettings.MinScaleOnWindow, settings.ScaleSettings.MaxScaleOnWindow);

        scaleShadow.transform.localScale += new Vector3(0.3f, 0.3f);
        ScaleChangeScript.ChangeOnWindow(scaleShadow.transform, settings.ScaleSettings.MinScaleOnWindow, settings.ScaleSettings.MaxScaleOnWindow);

        ChageRadiusCollider(fruitSettings.RadiusCollider);
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
        if (gameObject.activeSelf && transform.position.y < halfHeight && onVisible)
        {
            if (player != null)
                player.SubstractHealth(1);
            Destroy(gameObject);
        }

        if (transform.position.y < 5)
            Destroy(gameObject);
    }
}
