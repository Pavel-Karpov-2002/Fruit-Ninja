using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Magnet : Entity
{
    [SerializeField] private GameSettings gameSettings;

    private MagnetSettings _magnetSettings;
    private float timer;
    private bool isSlice;

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

        _magnetSettings = gameSettings.MagnetSettings;

        transform.DORotate(new Vector3(0, 0, 180), gameSettings.SpeedRotate).SetLoops(-180, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
            heightSprite = (GetComponent<SpriteRenderer>().sprite.bounds.size.y) / 2;

        blade.Entitys.Add(this);

        timer = _magnetSettings.AttractionTime;
    }

    private void FixedUpdate()
    {
        ScaleChangeScript.ChangeOnWindow(transform, gameSettings.ScaleSettings.MinScaleOnWindow, gameSettings.ScaleSettings.MaxScaleOnWindow);

        ChageRadiusCollider(_magnetSettings.RadiusCollider);
    }

    private void Update()
    {
        if(isSlice && !SliceCheckScript.BlockSlice)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Physics physicsMagnet = GetComponent<Physics>();
            physicsMagnet.Gravity = gameSettings.Gravity;

            blade.Entitys.Remove(this);
        }
    }

    public override void Destruction()
    {
        Physics physicsMagnet = GetComponent<Physics>();

        physicsMagnet.Gravity = 0;

        if (!isSlice)
        {
            AddBlob(_magnetSettings.MinBlobSize, 
                _magnetSettings.MaxBlobSize, 
                _magnetSettings.TimeLiveBlob, 
                _magnetSettings.BlobSprite, 
                gameSettings.BlobSettings);

            isSlice = true;
        }

        foreach (var entity in blade.Entitys)
        {
            if (entity != null)
            {
                if(Distance(entity) <= _magnetSettings.RadiusAttraction)
                {
                    entity.GetComponent<Physics>().Gravity = 0;

                    StartCoroutine(Step(entity));
                }
            }
        }
    }

    private float Distance(Entity entity)
    {
        return Vector3.Distance(transform.position, entity.transform.position);
    }

    private IEnumerator Step(Entity entity)
    {
        yield return null;

        if (entity != null)
        {
            Vector3 delta = entity.transform.position - transform.position;
            delta.Normalize();

            float moveSpeed = _magnetSettings.SpeedAttraction * Time.deltaTime;

            entity.transform.position = entity.transform.position - (delta * moveSpeed);

            if (timer > 0)
                StartCoroutine(Step(entity));
            else
            {
                Physics entityPhyics = entity.GetComponent<Physics>();

                entityPhyics.TimeLive = 0;
                entityPhyics.Impuls = 0;
                entityPhyics.Gravity = gameSettings.Gravity;
                entityPhyics.StartPosition = entity.transform.position;
            }
        }
    }
}
