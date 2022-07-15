using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected GamePlayEvents player;

    private void OnBecameInvisible()
    {
        float halfHeight = WorldSizeCamera.HalfHeight;

        if (gameObject.activeSelf && transform.position.y < halfHeight)
        {
            if (player != null)
                player.SubstractHealth(1);

            Destroy(gameObject);
        }
    }
}
