using UnityEngine;
using TMPro;

public class DemonstrationPoints : MonoBehaviour
{

    public static void Demonstration(GameObject gameObject, string count, GameObject textStyle, TextMeshProSettings text, bool isGradient, Color color)
    {
        GameObject points = Instantiate(textStyle);


        var bounds = gameObject.GetComponent<SpriteRenderer>().sprite.bounds;

        points.GetComponent<MeshRenderer>().material = text.TextPointsMaterial;

        Vector2 posObject = gameObject.transform.position;

        float width = (bounds.min.x - bounds.max.x) / 2;
        float height = (bounds.min.y - bounds.max.y) / 2;

        points.transform.position = new Vector3(Random.Range(posObject.x - width, posObject.x + width), Random.Range(posObject.y, posObject.y - height),0);
        points.GetComponent<TextMeshPro>().text = count.ToString();

        if (!isGradient)
        {
            points.GetComponent<TextMeshPro>().enableVertexGradient = false;
            points.GetComponent<TextMeshPro>().color = color;
        }

        ScaleChangeScript.Change(points.transform, 0f, 4);
        Destroy(points, 4);
    }
}
