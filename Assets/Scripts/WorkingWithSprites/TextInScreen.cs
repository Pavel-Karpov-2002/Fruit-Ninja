using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextInScreen : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings; 

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMeshPro sourceTMPro;


    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();

        sourceTMPro = gameSettings.TextMeshProSettings.TextPointsStyle;
        meshRenderer.material = gameSettings.TextMeshProSettings.TextPointsMaterial;

        Destroy(gameObject, gameSettings.TextMeshProSettings.TimeLive);
    }

    public void Demonstration(SpriteRenderer gameObject, string text, bool isGradient, Color color)
    {
        var bounds = gameObject.sprite.bounds;
        
        Vector2 posObject = gameObject.transform.position;

        float width = (bounds.min.x - bounds.max.x) / 2;
        float height = (bounds.min.y - bounds.max.y) / 2;

        transform.position = new Vector3(Random.Range(posObject.x - width, posObject.x + width), Random.Range(posObject.y, posObject.y - height),0);
        sourceTMPro.text = text.ToString();


        if (!isGradient)
        {
            sourceTMPro.enableVertexGradient = false;
            sourceTMPro.color = color;
        }
        
        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOScale(0f, gameSettings.TextMeshProSettings.TimeLive - 0.1f).SetEase(Ease.Linear));
    }

    private void OnDestroy()
    {
        DOTween.Kill(_sequence);
    }
}
