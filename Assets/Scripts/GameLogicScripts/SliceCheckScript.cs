using UnityEngine;

public class SliceCheckScript : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    private Vector2 _lastMousePos;
    private float _axisX;
    private float _axisY;

    private void Update()
    {
        if (Input.GetMouseButton(0) && CoreValues.HealthCount > 0)
        {
            OnTriggerCollider();
        }
        else
        {
            _axisX = 0;
            _axisY = 0;
        }

        GetSpeedMouse();
    }

    private void GetSpeedMouse()
    {
        if (_lastMousePos == Vector2.zero)
        {
            _lastMousePos = Input.mousePosition;
        }
        else
        {
            _axisX = ((Input.mousePosition.x - _lastMousePos.x) / Time.deltaTime) / Screen.width;
            _axisY = ((Input.mousePosition.y - _lastMousePos.y) / Time.deltaTime) / Screen.height;
            _lastMousePos = Input.mousePosition;
        }
    }

    private void OnTriggerCollider()
    {
        int countUnits = PullObjects.Units.Count;
        for (int i = 0; i < countUnits; i++)
        {
            if (CheckSliceCollider(PullObjects.Units[i]))
            {
                break;
            }
        }
    }

    protected bool CheckSliceCollider(Unit entity)
    {
        bool speeding = (Mathf.Abs(_axisX) > settings.SpeedSlice || Mathf.Abs(_axisY) > settings.SpeedSlice);

        if (entity.Slice.StartEntry == Vector2.zero && entity.Slice.EndEntry == Vector2.zero && speeding)
        {
            entity.Slice.StartEntry = entity.ColliderSphere.GetPositionMouse();
        }
        else if (entity.Slice.StartEntry != Vector2.zero && speeding)
        {
            entity.Slice.EndEntry = entity.ColliderSphere.GetPositionMouse();
            if (entity.ColliderSphere.HittingCollider(entity.RadiusCollider, entity.Slice.StartEntry, entity.Slice.EndEntry))
            {
                entity.Destruction();
                return true;
            }
        }
        else
        {
            entity.Slice.StartEntry = Vector2.zero;
            entity.Slice.EndEntry = Vector2.zero;
        }
        return false;
    }
}
