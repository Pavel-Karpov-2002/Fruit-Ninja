using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private void Awake()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        float halfHeight = WorldSizeCamera.HalfHeight;
        float halfWidth = WorldSizeCamera.HalfWidth;

        transform.position = new Vector3(halfWidth / 2, halfHeight / 2, transform.position.z);
    }
}
