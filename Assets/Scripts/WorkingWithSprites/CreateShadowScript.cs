using UnityEngine;

public class CreateShadowScript : MonoBehaviour
{
    public static void CreateShadow(GameObject gameObject)
    {
        GameObject child = new GameObject() { name = "Shadow" };
        SetTransform(child, gameObject);

        child.AddComponent<CreateShadowScript>();
        child.AddComponent<SpriteRenderer>();
        child.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        child.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.60f);
    }

    private static void SetTransform(GameObject child, GameObject gameObject)
    {
        child.transform.SetParent(gameObject.transform);
        child.transform.position = gameObject.transform.position;
        child.transform.localScale = gameObject.transform.localScale;
        child.transform.rotation = gameObject.transform.rotation;
        child.transform.localPosition = new Vector3(0.2f, 0.15f, 1);
    }
}
