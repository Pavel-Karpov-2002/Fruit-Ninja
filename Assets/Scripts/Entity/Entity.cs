using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected GamePlayEvents player;
    private ColliderSphere colliderSphere;
    private SliceRange slice;
    private float radiusCollider;

    public float RadiusCollider
    {
        get { return radiusCollider; }
        set { radiusCollider = value; }
    }

    public ColliderSphere ColliderSphere
    {
        get { return colliderSphere; }
        set { colliderSphere = value; }
    }

    public SliceRange Slice
    {
        get { return slice; }
        set { slice = value; }
    }

    public abstract void Destruction();

    public void Trow(float angle, float impuls, float g, Vector3 startPosition)
    {
        if (gameObject.GetComponent<Physics>() != null)
            gameObject.GetComponent<Physics>().AddImpulse(angle, impuls, g, startPosition);
        else
            Debug.Log($"{gameObject.name} does not have a Physics class");
    }
}
