using UnityEngine;

public class CreateShadowScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sourceSprite;
    [SerializeField] private SpriteRenderer shadow;

    private void Start()
    {
        CreateShadow();
    }

    public void CreateShadow()
    {
        shadow.sprite = sourceSprite.sprite;
        shadow.color = new Color(0, 0, 0, 0.60f);
        transform.localPosition = new Vector3(0.2f, 0.15f, 1);
    }
}
