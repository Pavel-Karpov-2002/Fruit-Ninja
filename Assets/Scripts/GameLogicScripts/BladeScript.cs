using UnityEngine;

public class BladeScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TrailRenderer sourceTrailRenderer;

    private void Start()
    {
        sourceTrailRenderer.enabled = false;
        MoveThis();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && CoreValues.HealthCount > 0)
        {
            MoveThis();
            sourceTrailRenderer.enabled = true;
        }
        else
        {
            sourceTrailRenderer.Clear();
            sourceTrailRenderer.enabled = false;
        }
    }

    private void MoveThis()
    {
        transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
