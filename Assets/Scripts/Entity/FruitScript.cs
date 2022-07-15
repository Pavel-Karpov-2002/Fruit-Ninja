using UnityEngine;
using DG.Tweening;
using TMPro;

public class FruitScript : Entity
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private float speedRotate = 1.5f;

    private float halfHeight;
    private float radiusCollider;
    private ColliderSphere colliderSphere;
    private SliceRange slice;
    private Transform scaleShadow;
    private int numFruit;

    private void Awake()
    {
        player = FindObjectOfType<GamePlayEvents>();

        numFruit = Random.Range(0, settings.FruitSprites.Count);

        FruitSprite fruit = settings.FruitSprites[numFruit];
        gameObject.GetComponent<SpriteRenderer>().sprite = fruit.FruitsSprite;

        colliderSphere = GetComponent<ColliderSphere>();

        slice = GetComponent<SliceRange>();

        CreateShadowScript.CreateShadow(gameObject);

        if (colliderSphere == null)
        {
            Debug.Log("Object don't have a collider");
        }

        if (slice == null)
        {
            Debug.Log("Object don't have a slice script");
        }
    }

    private void Start()
    {
        CreateShadowScript shadow = gameObject.GetComponentInChildren<CreateShadowScript>();
        scaleShadow = shadow.transform;

        halfHeight = WorldSizeCamera.HalfHeight;

        transform.DORotate(new Vector3(0, 0, 180), speedRotate).SetLoops(-180, LoopType.Incremental).SetEase(Ease.Linear);

        NormalizeObject();
    }

    private void FixedUpdate()
    {
        NormalizeObject();
    }

    private void Update()
    {
        /* for (int i = 0; i < Input.touchCount; ++i)
         {
             if ((Input.GetTouch(i).phase == TouchPhase.Began || Input.GetMouseButton(0)) && CoreValues.HealthCount > 0)
                 OnTriggerCollider();
         }*/

        if (Input.GetMouseButton(0) && CoreValues.HealthCount > 0)
            OnTriggerCollider();
    }

    private void NormalizeObject()
    {
        ChangeScale();

        ChageRadius();
    }


    private void ChangeScale()
    {
        float normalScale = ((transform.position.y) / halfHeight);

        if(normalScale > 0.86f)
        {
            normalScale = 0.86f;
        }else if (normalScale < 0.5f)
        {
            normalScale = 0.5f;
        }

        ScaleChangeScript.Change(transform, normalScale, 0);

        ScaleChangeScript.Change(scaleShadow, normalScale + 0.3f, 0);
    }

    private void ChageRadius()
    {
        radiusCollider = (transform.localScale.y / GetComponent<SpriteRenderer>().bounds.size.y) * 1.3f;
    }

    public void Trow(float angle, float impuls, float g, Vector3 startPosition)
    {
        if (gameObject.GetComponent<Physics>() != null)
            gameObject.GetComponent<Physics>().AddImpulse(angle, impuls, g, startPosition);
        else
            Debug.Log("Object does not have a Physics class");
    }

    private void OnTriggerCollider()
    {
        if (colliderSphere.HittingCollider(radiusCollider))
            slice.StartEntry = colliderSphere.GetLengthVector();
        else if(slice.StartEntry != 0 && slice.EndEntry == 0)
        {
            slice.EndEntry = colliderSphere.GetLengthVector();
            if (slice.FruitIsCut(radiusCollider))
            {
                player.AddPoint(settings.NumberOfPointsPerFruit, gameObject);
                CutSpriteScript.GetTwoHalves(gameObject.GetComponent<SpriteRenderer>().sprite.texture, gameObject);

                BlobSettings blobSetting = settings.BlobSettings;

                CreateBlobScript.CreateMoreBlub(gameObject, 
                    settings.FruitSprites[numFruit].BlobSprite, 
                    settings);
                
                CreateBlobScript.CreateOneBlob(
                    gameObject, 
                    settings.FruitSprites[numFruit].BlobSprite, 
                    blobSetting.MinBlobScale, 
                    blobSetting.MaxBlobScale, 
                    blobSetting.BlobSpeed, 
                    blobSetting.BlobDelayTime,
                    blobSetting.LayerBlob);

                Destroy(gameObject);
            }
        }
        else
        {
            slice.StartEntry = 0;
            slice.EndEntry = 0;
        }
    }
}
