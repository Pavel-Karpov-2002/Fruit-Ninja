using UnityEngine;

public class CutSpriteScript : MonoBehaviour
{
    private static Sprite[] Cut(Texture2D texture)
    {
        Sprite[] halves = new Sprite[2];

        var rect = new Rect(texture.width / 2, 0, texture.width / 2, texture.height);
        halves[0] = Sprite.Create(texture, rect, Vector2.up * 0.5f);

        rect = new Rect(0, 0, texture.width / 2, texture.height);
        halves[1] = Sprite.Create(texture, rect, new Vector2(1, 0.5f));
        return halves;
    }

    public static void GetTwoHalves(Texture2D texture, GameObject gameObject)
    {
        Sprite[] helves = Cut(texture);

        for(int i = 0; i < helves.Length; i++)
        {
            GameObject halve = new GameObject() { name = "Halve " + i };
            Transform transformObj = gameObject.transform;
            Transform transformHalve = halve.transform;

            transformHalve.position = transformObj.position;
            transformHalve.localScale = transformObj.localScale;
            transformHalve.rotation = transformObj.rotation;

            halve.AddComponent<SpriteRenderer>();
            halve.GetComponent<SpriteRenderer>().sprite = helves[i];

            CreateShadowScript.CreateShadow(halve);
            ScaleChangeScript.Change(halve.GetComponentInChildren<CreateShadowScript>().transform, 1f, 0);

            halve.AddComponent<Physics>();
            halve.GetComponent<Physics>().AddImpulse(Random.Range(40, 120), 1.8f, 1.2f, halve.transform.position);

            Destroy(halve, 5);
        }

    }
}
