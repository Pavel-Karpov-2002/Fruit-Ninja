using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FreezingBonus : Entity
{
    [SerializeField] private GameSettings gameSettings;

    private FreezingSettings _freezingSettings;

    private void Awake()
    {
        player = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerEntitys>();

        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider!");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script!");

        _freezingSettings = gameSettings.FreezingSettings;
    }

    private void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
            heightSprite = (GetComponent<SpriteRenderer>().sprite.bounds.size.y) / 2;

        player.Entitys.Add(this);

        transform.DORotate(new Vector3(0, 0, 180), gameSettings.SpeedRotate).SetLoops(-180, LoopType.Incremental).SetEase(Ease.Linear);
    }


    private void FixedUpdate()
    {
        ScaleChangeScript.ChangeOnWindow(transform, gameSettings.ScaleSettings.MinScaleOnWindow, gameSettings.ScaleSettings.MaxScaleOnWindow);

        ChageRadiusCollider(_freezingSettings.RadiusCollider);
    }


    public override void Destruction()
    {
        StartCoroutine(Freezing());

        Destroy(gameObject);
    }

    private IEnumerator Freezing()
    {
        AddBlob();
        CutSpriteScript.GetTwoHalves(gameObject.GetComponent<SpriteRenderer>().sprite.texture, gameObject);

        Time.timeScale = _freezingSettings.TimeScale;

        yield return new WaitForSeconds(_freezingSettings.TimeReduction);

        Time.timeScale = 1;
    }

    private void AddBlob()
    {
        GameObject newBlob = new GameObject() { name = "BlobFreezing" };

        newBlob.transform.position = gameObject.transform.position;
        newBlob.transform.localScale = new Vector2(_freezingSettings.MaxBlobSize, _freezingSettings.MaxBlobSize);

        newBlob.AddComponent<SpriteRenderer>();

        newBlob.GetComponent<SpriteRenderer>().sprite = _freezingSettings.BlobSprite;

        ScaleChangeScript.Change(newBlob.transform, _freezingSettings.MinBlobSize, _freezingSettings.TimeLiveBlob);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf && transform.position.y < WorldSizeCamera.HalfHeight)
        {
            Destroy(gameObject);
        }
    }
}
