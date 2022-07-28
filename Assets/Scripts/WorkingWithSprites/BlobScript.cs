using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class BlobScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sourceSprite;
    [SerializeField] private GameSettings gameSettings;

    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();
    }

    public void CreateSourceBlob(Sprite blobSprite, float scale, float speed, GameObject entity)
    {
        SetTransform(gameSettings.BlobSettings.LayerBlob, entity);

        DecreaseObject(blobSprite, scale, speed);

        Destroy(gameObject, speed + 0.1f);
    }

    private void DecreaseObject(Sprite blobSprite, float scale, float speed)
    {
        transform.localScale = new Vector3(scale, scale);

        sourceSprite.sprite = blobSprite;

        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOScale(0, speed));
        _sequence.Append(sourceSprite.material.DOFade(0, speed));
    }

    public void CreateOneBlob(Sprite blobSprite, float scale, float speed, GameObject entity, Sprite source)
    {
        SetTransform(gameSettings.BlobSettings.LayerBlob, entity, source);

        SpreadingEffect(blobSprite, scale, speed);

        Destroy(gameObject, speed + 0.1f);
    }

    private void SpreadingEffect(Sprite blobSprite, float scale, float speed)
    {
        transform.localScale = new Vector3(scale, scale);

        sourceSprite.sprite = blobSprite;

        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOScaleY(scale + gameSettings.BlobSettings.MaxBlobScale, speed));
        _sequence.Append(sourceSprite.material.DOFade(0, speed));
    }

    private void SetTransform(float z, GameObject entity)
    {
        Vector2 posObject = entity.transform.position;

        transform.position =
            new Vector3(
                posObject.x,
                posObject.y,
                z);

        transform.localScale = entity.transform.localScale;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void SetTransform(float z, GameObject entity, Sprite source)
    {
        var bounds = source.bounds;
        Vector2 posObject = entity.transform.position;

        float width = (bounds.min.x - bounds.max.x) / 2;
        float height = (bounds.min.y - bounds.max.y) / 2;

        transform.position = 
            new Vector3(
                Random.Range(posObject.x - width, posObject.x + width), 
                Random.Range(posObject.y, posObject.y - height), 
                z);

        transform.localScale = entity.transform.localScale;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
