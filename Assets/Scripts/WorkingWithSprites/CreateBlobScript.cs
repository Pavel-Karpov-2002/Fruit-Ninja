using UnityEngine;

public class CreateBlobScript : MonoBehaviour
{
    public static void CreateOneBlob(GameObject gameObject, Sprite sprite, float minBlobScale, float maxBlobScale, float blobSpeed, float blobDelay, float layer)
    {
        GameObject child = new GameObject() { name = "Blob" };
        SetTransform(child, gameObject, layer);

        child.AddComponent<SpriteRenderer>();
        child.GetComponent<SpriteRenderer>().sprite = sprite;

        child.transform.localScale = new Vector2(maxBlobScale, maxBlobScale);

        ScaleChangeScript.DelayedChange(child.transform, minBlobScale, blobSpeed, blobDelay);

        Destroy(child, blobDelay + blobSpeed);
    }

    public static void CreateMoreBlub(GameObject gameObject, Sprite blobSprite, GameSettings settings)
    {
        BlobSettings blobSettings = settings.BlobSettings;
        int coungBlob = Random.Range(blobSettings.MinBlobBackground, blobSettings.MaxBlobBackground);



        for (int i = 0; i < coungBlob; i++)
        {
            float scale = Random.Range(blobSettings.MinBlobBackgroundScale, blobSettings.MaxBlobBackgroundScale);

            CreateOneBlob(gameObject, blobSprite, blobSettings.MinBlobBackgroundScale, scale, blobSettings.BlobBackgroundSpeed, blobSettings.BlobBackgroundDelayTime, blobSettings.LayerBlobBackground);
        }
    }

    private static void SetTransform(GameObject child, GameObject gameObject, float z)
    {
        var bounds = gameObject.GetComponent<SpriteRenderer>().sprite.bounds;
        Vector2 posObject = gameObject.transform.position;

        float width = (bounds.min.x - bounds.max.x) / 2;
        float height = (bounds.min.y - bounds.max.y) / 2;

        child.transform.position = 
            new Vector3(
                Random.Range(posObject.x - width, posObject.x + width), 
                Random.Range(posObject.y, posObject.y - height), 
                z);

        child.transform.localScale = gameObject.transform.localScale;
        child.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
