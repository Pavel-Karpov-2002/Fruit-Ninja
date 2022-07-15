using UnityEngine;

public abstract class Collider : MonoBehaviour
{
    protected Vector3 GetPositionMouse()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        return ScreenCoordinatesToWorld.ScreenToWorld(mouse);
    }

    public float GetLengthVector()
    {
        Vector3 pos = transform.position;
        Vector3 mouse = GetPositionMouse();

        return Mathf.Sqrt(GetDiffOfSquares(mouse.x, pos.x) + GetDiffOfSquares(mouse.y, pos.y) + GetDiffOfSquares(mouse.z, pos.z));
    }

    private float GetDiffOfSquares(float p1, float p2)
    {
        return Mathf.Pow(p1 - p2, 2);
    }
}
