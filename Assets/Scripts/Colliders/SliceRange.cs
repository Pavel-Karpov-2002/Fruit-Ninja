using UnityEngine;

public class SliceRange : MonoBehaviour
{
    private Vector2 startEntry = Vector2.zero;
    private Vector2 endEntry = Vector2.zero;

    public Vector2 StartEntry
    {
        get { return startEntry; }
        set { startEntry = value; }
    }

    public Vector2 EndEntry
    {
        get { return endEntry; }
        set { endEntry = value; }
    }
}
