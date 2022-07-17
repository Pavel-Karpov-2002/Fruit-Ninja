using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected GamePlayEvents blade;
    protected SpawnerEntitys[] spawners;

    protected float[] gravity;
    protected float heightSprite;

    private ColliderSphere _colliderSphere;
    private SliceRange _slice;

    private float _radiusCollider;

    public float RadiusCollider
    {
        get { return _radiusCollider; }
        set { _radiusCollider = value; }
    }

    public ColliderSphere ColliderSphere
    {
        get { return _colliderSphere; }
        set { _colliderSphere = value; }
    }

    public SliceRange Slice
    {
        get { return _slice; }
        set { _slice = value; }
    }

    private void Awake()
    {
        blade = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerEntitys>();
    }

    private void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
            heightSprite = (GetComponent<SpriteRenderer>().sprite.bounds.size.y) / 2;

        blade.Entitys.Add(this);
    }

    public abstract void Destruction();

    public void Trow(float angle, float impuls, float g, Vector3 startPosition)
    {
        if (gameObject.GetComponent<Physics>() != null)
            gameObject.GetComponent<Physics>().AddImpulse(angle, impuls, g, startPosition);
        else
            Debug.Log($"{gameObject.name} does not have a Physics class");
    }

    protected virtual void ChageRadiusCollider(float collider)
    {
        if (collider == 0)
            RadiusCollider = heightSprite * transform.localScale.y;
        else
            RadiusCollider = collider * transform.localScale.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, RadiusCollider);
    }

    protected void StopAllPhysicsEntity()
    {
        SliceCheckScript.BlockSlice = true;

        int countEntitys = blade.Entitys.Count;

        gravity = new float[countEntitys];


        for (int i = 0; i < countEntitys; i++)
        {
            if (blade.Entitys[i] == null)
                continue;

            var physics = blade.Entitys[i].GetComponent<Physics>();

            gravity[i] = physics.Gravity;

            physics.Gravity = 0;
        }

        SpawnerActive(false);
    }

    protected virtual void StartAllPhysics()
    {
        SliceCheckScript.BlockSlice = false;

        for (int i = 0; i < blade.Entitys.Count; i++)
        {
            if (blade.Entitys[i] == null)
                continue;

            if (blade.Entitys[i] != this)
            {
                blade.Entitys[i].GetComponent<Physics>().Gravity = gravity[i];
            }
        }

        SpawnerActive(true);
    }

    protected virtual void AddBlob(float minSize, float maxSize, float timeScale, Sprite spriteBlob, BlobSettings blobSettings)
    {

        Debug.Log(gameObject);
        CreateBlobScript.CreateOneBlob(
            gameObject,
            spriteBlob,
            minSize,
            maxSize,
            timeScale,
            blobSettings.BlobDelayTime,
            blobSettings.LayerBlob);
    }

    private void SpawnerActive(bool set)
    {
        foreach (SpawnerEntitys spawner in spawners)
        {
            if (spawner != null)
                spawner.gameObject.SetActive(set);
        }
    }
}
