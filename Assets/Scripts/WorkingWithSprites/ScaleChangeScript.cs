using UnityEngine;

public class ScaleChangeScript : MonoBehaviour
{
    public static float OnWindow(Transform transform, float startScale, float minScale)
    {
        float normalScale = startScale - startScale * Mathf.Lerp(0.1f, 1, 1f / Vector2.Distance(transform.position, new Vector2(WorldSizeCamera.HalfWidth / 2, 0)));

        if (normalScale < minScale)
            normalScale = minScale;

        return normalScale;
    }
}
