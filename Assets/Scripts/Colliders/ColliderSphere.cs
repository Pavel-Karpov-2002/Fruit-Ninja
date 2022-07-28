using UnityEngine;

public class ColliderSphere : Collider
{
    public bool HittingCollider(float radius)
    {
        if (OnCollision <= radius)
        {
            return true;
        }
        return false;
    }
}
