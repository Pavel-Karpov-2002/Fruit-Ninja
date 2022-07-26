using System.Collections;
using UnityEngine;

public abstract class Unit : Entity
{
    [SerializeField] private BlobScript blob;
    [SerializeField] private GameSettings settings;
    [SerializeField] private ColliderSphere colliderSphere;

    private float _radiusCollider;

    public float RadiusCollider
    {
        get { return _radiusCollider; }
        set { _radiusCollider = value; }
    }

    public ColliderSphere ColliderSphere
    {
        get { return colliderSphere; }
        set { colliderSphere = value; }
    }

    public GameSettings Settings
    {
        get { return settings; }
        set { settings = value; }
    }

    private void Start()
    {
        StartScale = transform.localScale.y;

        HeightSprite = (SourceSprite.sprite.bounds.size.y) / 2;
    }

    public abstract void Destruction();

    protected virtual void ChageRadiusCollider(float collider)
    {
        if (collider == 0)
            RadiusCollider = HeightSprite * transform.localScale.y;
        else
            RadiusCollider = collider * transform.localScale.y;
    }

    protected void BlobCreate(Sprite blobSprite, float countBlobs)
    {
        BlobSettings blobSettings = settings.BlobSettings;

        for (int i = 0; i < countBlobs; i++)
        {
            BlobScript newBlob = Instantiate(blob);

            float scale = Random.Range(blobSettings.MinBlobScale, blobSettings.MaxBlobScale);

            float speed = Random.Range(blobSettings.MinBlobSpeedDisappearance, blobSettings.MaxBlobSpeedDisappearance);

            newBlob.CreateOneBlob(blobSprite, scale, speed, gameObject, SourceSprite.sprite);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadiusCollider);
    }

    protected void ChangeScaleOnWindow()
    {
        float scale = ScaleChangeScript.OnWindow(transform, StartScale, StartScale + settings.ScaleSettings.MaxScaleOnWindow);

        transform.localScale = new Vector2(scale, scale);
    }

    protected IEnumerator OutOfBounds()
    {
        if (transform.position.y < -(WorldSizeCamera.HalfHeight / 3))
        {
            PullObjects.Units.Remove(this);
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(OutOfBounds());
    }
}
