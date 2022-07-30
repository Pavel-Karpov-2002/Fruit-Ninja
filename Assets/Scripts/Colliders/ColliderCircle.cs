using UnityEngine;

public class ColliderCircle : Collider
{
    public bool HittingCollider(float radius)
    {
        if (GetLengthVector() <= radius)
            return true;
        return false;
    }
}
