using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private SliceRange slice;
    [SerializeField] private SpriteRenderer sourceSprite;
    [SerializeField] private Physics sourcePhysics;

    private float _heightSprite;
    private float _startScale;

    public SpriteRenderer SourceSprite
    {
        get { return sourceSprite; }
        set { sourceSprite = value; }
    }

    public SliceRange Slice
    {
        get { return slice; }
        set { slice = value; }
    }

    public Physics SourcePhysics
    {
        get { return sourcePhysics; }
        set { sourcePhysics = value; }
    }

    public float StartScale
    {
        get { return _startScale; }
        set { _startScale = value; }
    }

    public float HeightSprite
    {
        get { return _heightSprite; }
        set { _heightSprite = value; }
    }

    private void Start()
    {
        _startScale = transform.localScale.y;

        _heightSprite = (SourceSprite.sprite.bounds.size.y) / 2;
    }

    public void Trow(float angle, float impuls, float g, Vector3 startPosition)
    {
        SourcePhysics.AddImpulse(angle, impuls, g, startPosition);
    }
}
