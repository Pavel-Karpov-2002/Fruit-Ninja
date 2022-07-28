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
                countUnits = PullObjects.Units.Count;
                i = 0;
            }
        }
    }

    protected bool CheckSliceCollider(Unit entity)
    {
        bool speeding = (Mathf.Abs(_axisX) > settings.SpeedSlice || Mathf.Abs(_axisY) > settings.SpeedSlice);

        if (entity.ColliderSphere.HittingCollider(entity.RadiusCollider) && speeding)
        {
            entity.Slice.StartEntry = entity.ColliderSphere.OnCollision;
        }
        else if (entity.Slice.StartEntry != 0 && entity.Slice.EndEntry == 0 && speeding)
        {
            entity.Slice.EndEntry = entity.ColliderSphere.OnCollision;
            if (entity.Slice.IsCut(entity.RadiusCollider))
            {
                entity.Destruction();
                return true;
            }
        }
        else
        {
            entity.Slice.StartEntry = 0;
            entity.Slice.EndEntry = 0;
        }
        return false;
    }
}
