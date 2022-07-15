using UnityEngine;

public class ScreenCoordinatesToWorld : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Vector3 pos, float? z = null)
    {
        Camera camera = Camera.main;

        if (camera.orthographic)
        {
            var p = camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, camera.nearClipPlane));
            p.z = z != null ? (float)z : 0f;
            return p;
        }
        else
        {
            var ray = camera.ScreenPointToRay(new Vector3(pos.x, pos.y, camera.nearClipPlane));
            var d = z != null ? (float)z : 20f;
            var p = camera.transform.position + ray.direction * d;
            return p;
        }
    }
}
