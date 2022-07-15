using UnityEngine;

public class SliceCheckScript : MonoBehaviour
{
    [SerializeField] private GamePlayEvents player;

    private static bool blockSlice;

    public static bool BlockSlice
    {
        get { return blockSlice; }
        set { blockSlice = value; }
    }

    public void Update()
    {
        /* for (int i = 0; i < Input.touchCount; ++i)
         {
             if ((Input.GetTouch(i).phase == TouchPhase.Began || Input.GetMouseButton(0)) && CoreValues.HealthCount > 0)
                 OnTriggerCollider();
         }*/

        if (Input.GetMouseButton(0) && CoreValues.HealthCount > 0 && !blockSlice)
            OnTriggerCollider();
    }

    private void OnTriggerCollider()
    {
        foreach (Entity entity in player.Entitys)
        {
            if (CheckSliceCollider(entity))
                break;
        }
    }

    private bool CheckSliceCollider(Entity entity)
    {
        if (entity.ColliderSphere != null)
        {
            if (entity.ColliderSphere.HittingCollider(entity.RadiusCollider))
            {
                entity.Slice.StartEntry = entity.ColliderSphere.GetLengthVector();
            }
            else if (entity.Slice.StartEntry != 0 && entity.Slice.EndEntry == 0)
            {
                entity.Slice.EndEntry = entity.ColliderSphere.GetLengthVector();
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
        }

        return false;
    }
}
