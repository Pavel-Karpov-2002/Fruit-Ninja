using UnityEngine;

public class CreateShadowScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sourceSprite;
    [SerializeField] private SpriteRenderer shadow;
    [SerializeField] private Transform parentTransform;

    private Vector3 _scale;

    private void Start()
    {
        CreateShadow();
    }

    private void Update()
    {
        _scale = parentTransform.localScale;
        transform.localPosition = new Vector2(_scale.x / 4, _scale.y / 4);
    }

    public void CreateShadow()
    {
        shadow.sprite = sourceSprite.sprite;
        shadow.color = new Color(0, 0, 0, 0.60f);
        transform.localPosition = new Vector3(0.2f, 0.15f, 1);
    }
}
