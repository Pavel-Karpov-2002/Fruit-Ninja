
public class ColliderSphere : Collider
{
    public bool HittingCollider(float radius)
    {
        if (GetLengthVector() <= radius)
        {
            return true;
        }
        
        return false;
    }
}
