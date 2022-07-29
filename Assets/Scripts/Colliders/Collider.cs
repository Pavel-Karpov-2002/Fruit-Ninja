using UnityEngine;

public abstract class Collider : MonoBehaviour
{
    public Vector2 GetPositionMouse()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        return ScreenCoordinatesToWorld.ScreenToWorld(mouse);
    }

    public float GetLengthVector()
    {
        Vector3 pos = transform.position;
        Vector3 mouse = GetPositionMouse();

        return Vector2.Distance(pos, mouse);
    }
}
