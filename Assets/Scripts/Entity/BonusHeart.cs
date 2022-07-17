using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class BonusHeart : Entity
{
    [SerializeField] private GameSettings gameSettings;

    private HealthSettings _healthSettings;
    private HeartUI panel;
    private bool isFly;

    private void Awake()
    {
        blade = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerEntitys>();

        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        panel = blade.HealthPanel.GetComponent<HeartUI>();

        if (panel == null)
            Debug.Log($"{gameObject.name} --> {panel.name} don't have a HeartUI script!");

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider!");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script!");

        _healthSettings = gameSettings.HealthSettings;
    }

    private void Start()
    {
        heightSprite = (GetComponent<SpriteRenderer>().sprite.bounds.size.y) / 2;
        blade.Entitys.Add(this);
        GetComponent<Physics>().Impuls += _healthSettings.IncreaseImpuls;
    }

    private void FixedUpdate()
    {
        if (!isFly)
        {
            ScaleChangeScript.ChangeOnWindow(transform, gameSettings.ScaleSettings.MinScaleOnWindow, gameSettings.ScaleSettings.MaxScaleOnWindow);

            ChageRadiusCollider(_healthSettings.RadiusCollider);
        }
    }

    public override void Destruction()
    {
        StartCoroutine(AddHealth());
    }

    private IEnumerator AddHealth()
    {
        StopAllPhysicsEntity();


        panel.AddHeart();
        panel.ChangeAlphaColorHeart(0, 0);

        blade.AddHealth(_healthSettings.CountHealthAdd);

        yield return new WaitForSeconds(0.1f);

        transform.rotation = Quaternion.Euler(0, 0, 0);

        AddBlob(_healthSettings.MinBlobSize, _healthSettings.MaxBlobSize, _healthSettings.TimeMoveHeartToHeartPanel, _healthSettings.BlobSprite, gameSettings.BlobSettings);

        HeartFly();


        yield return new WaitForSeconds(_healthSettings.TimeMoveHeartToHeartPanel);

        panel.ChangeAlphaColorHeart(0, 1);
        StartAllPhysics();

        blade.Entitys.Remove(this);
        Destroy(gameObject);
    }

    private void HeartFly()
    {
        gameObject.AddComponent<RectTransform>();
        gameObject.AddComponent<Image>();

        GetComponent<SpriteRenderer>().enabled = false;

        GetComponent<Image>().sprite = _healthSettings.HeartSpriteOnPanel;

        transform.SetParent(blade.MainCanvas.transform);

        isFly = true;
        transform.localScale = new Vector3(1, 1, 1);

        ScaleChangeScript.ChangeRectangleSize(GetComponent<RectTransform>(), panel.GetScaleHeart(0), _healthSettings.TimeMoveHeartToHeartPanel);

        transform.DOMove(new Vector2(panel.GetPositionHeartInWorldCoordinates(0).x - 0.2f, panel.GetPositionHeartInWorldCoordinates(0).y - 0.2f), 
            _healthSettings.TimeMoveHeartToHeartPanel);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf && transform.position.y < WorldSizeCamera.HalfHeight)
        {
            Destroy(gameObject);
        }
    }
}
