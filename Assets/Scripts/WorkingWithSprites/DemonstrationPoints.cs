using UnityEngine;
using TMPro;

public class DemonstrationPoints : MonoBehaviour
{

    public static void Demonstration(GameObject gameObject, int count, GameObject textStyle)
    {
        GameObject points = new GameObject() { name = "Points" };

        points.AddComponent<TextMeshPro>();

        var bounds = gameObject.GetComponent<SpriteRenderer>().sprite.bounds;

        TextMeshPro textPoints = points.GetComponent<TextMeshPro>();
        textPoints = textStyle.GetComponent<TextMeshPro>();

        Vector2 posObject = gameObject.transform.position;

        float width = (bounds.min.x - bounds.max.x) / 2;
        float height = (bounds.min.y - bounds.max.y) / 2;

        textPoints.transform.position = new Vector3(Random.Range(posObject.x - width, posObject.x + width), Random.Range(posObject.y, posObject.y - height),0);
        textPoints.text = count.ToString();

        ScaleChangeScript.Change(points.transform, 0f, 4);
        Destroy(points, 4);
    }
}
