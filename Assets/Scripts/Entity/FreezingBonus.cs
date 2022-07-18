using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FreezingBonus : Entity
{
    [SerializeField] private GameSettings gameSettings;

    private FreezingSettings _freezingSettings;
    private float timeFreezing;

    private void Awake()
    {
        blade = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerEntitys>();

        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider!");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script!");

        _freezingSettings = gameSettings.FreezingSettings;

        timeFreezing = _freezingSettings.TimeReduction;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));

        transform.DORotate(new Vector3(0, 0, 180), gameSettings.SpeedRotate).SetLoops(-180, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void FixedUpdate()
    {
        ScaleChangeScript.ChangeOnWindow(transform, gameSettings.ScaleSettings.MinScaleOnWindow, gameSettings.ScaleSettings.MaxScaleOnWindow);

        ChageRadiusCollider(_freezingSettings.RadiusCollider);
    }

    private void Update()
    {
        if (SliceCheckScript.BlockSlice)
            timeFreezing -= Time.deltaTime;
    }

    public override void Destruction()
    {
        StartCoroutine(Freezing());

        Destroy(gameObject);
    }

    private IEnumerator Freezing()
    {
        AddBlob(_freezingSettings.MinBlobSize, _freezingSettings.MaxBlobSize, _freezingSettings.TimeLiveBlob, _freezingSettings.BlobSprite, gameSettings.BlobSettings);

        CutSpriteScript.GetTwoHalves(gameObject.GetComponent<SpriteRenderer>().sprite.texture, gameObject);

        Time.timeScale = _freezingSettings.TimeScale;

        yield return null;
        
        if(timeFreezing <= 0)
            Time.timeScale = 1;
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf && transform.position.y < WorldSizeCamera.HalfHeight)
        {
            Destroy(gameObject);
        }
    }
}
