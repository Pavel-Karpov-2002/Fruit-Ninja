using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        float halfHeight = WorldSizeCamera.HalfHeight;
        float halfWidth = WorldSizeCamera.HalfWidth;

        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);

        SpriteRenderer sprRend = gameObject.GetComponent<SpriteRenderer>();
        sprRend.drawMode = SpriteDrawMode.Sliced;
        sprRend.size = new Vector2(halfWidth, halfHeight);
    }
}
