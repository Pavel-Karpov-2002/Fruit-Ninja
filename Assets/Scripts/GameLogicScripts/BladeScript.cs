using UnityEngine;

public class BladeScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        if (GetComponent<TrailRenderer>() == null)
        {
            Debug.Log($"{gameObject.name} don't have a TrailRenderer");
        }
        GetComponent<TrailRenderer>().enabled = false;
        Move();
    }

    private void FixedUpdate()
    {
        /* for (int i = 0; i < Input.touchCount; ++i)
         {
             if ((Input.GetTouch(i).phase == TouchPhase.Began || Input.GetMouseButton(0)) && CoreValues.HealthCount > 0 && !SliceCheckScript.BlockSlice)
                 OnTriggerCollider();
         }*/

        if (Input.GetMouseButton(0) && CoreValues.HealthCount > 0 && !SliceCheckScript.BlockSlice)
        {
            Move();
            GetComponent<TrailRenderer>().enabled = true;
        }
        else
            GetComponent<TrailRenderer>().enabled = false;
    }

    private void Move()
    {
        transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
