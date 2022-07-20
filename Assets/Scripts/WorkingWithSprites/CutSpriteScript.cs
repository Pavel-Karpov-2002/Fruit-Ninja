using UnityEngine;

public class CutSpriteScript : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private SpriteRenderer sourceSprite;
    [SerializeField] private Halve halve;

    private HalvesPhysicsSettings _halvesSettings;

    private void Start()
    {
        _halvesSettings = gameSettings.HalvesPhysicsSettings;
    }

    private Sprite[] Cut()
    {
        Sprite[] halves = new Sprite[2];

        float width = sourceSprite.sprite.texture.width;
        float height = sourceSprite.sprite.texture.height;
        Texture2D texture = sourceSprite.sprite.texture;

        var rect = new Rect(width / 2, 0, width / 2, height);
        halves[0] = Sprite.Create(texture, rect, Vector2.up * 0.5f);

        rect = new Rect(0, 0, width / 2, height);
        halves[1] = Sprite.Create(texture, rect, new Vector2(1, 0.5f));

        return halves;
    }

    public void CreateTwoHalves()
    {
        Sprite[] helves = Cut();

        for(int i = 0; i < helves.Length; i++)
        {
            Halve halves = Instantiate(halve, transform.position, transform.rotation);
            
            halves.transform.localScale = transform.localScale;

            halves.SourceSprite.sprite = helves[i];

            halves.Trow(Random.Range(_halvesSettings.MinAngle, _halvesSettings.MaxAngle), Random.Range(_halvesSettings.MinImpuls, _halvesSettings.MaxImpuls + 1), gameSettings.Gravity, halves.transform.position);
        }
    }
}
