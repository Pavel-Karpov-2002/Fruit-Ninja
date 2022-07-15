using UnityEngine;

public class WorldSizeCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private static float halfHeight;
    private static float halfWidth;

    public static float HalfHeight => halfHeight;
    public static float HalfWidth => halfWidth;

    private void Awake()
    {
        halfHeight = mainCamera.orthographicSize * 2;
        halfWidth = mainCamera.aspect * halfHeight;
    }
}
