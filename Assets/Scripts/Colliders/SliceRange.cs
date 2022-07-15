using UnityEngine;

public class SliceRange : MonoBehaviour
{
    private float startEntry;
    private float endEntry;

    public float StartEntry
    {
        get { return startEntry; }
        set { startEntry = value; }
    }

    public float EndEntry
    {
        get { return endEntry; }
        set { endEntry = value; }
    }

    public bool FruitIsCut(float radius)
    {
        if (startEntry + endEntry >= radius / 2)
        {
            return true;
        }

        return false;
    }
}