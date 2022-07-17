using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BombScript : Entity
{
    [SerializeField] private GameSettings gameSettings;

    private BombSettigns bombSettings;

    private void Awake()
    {
        blade = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerEntitys>(); 

        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script");

        bombSettings = gameSettings.BombSettings;
    }

    private void FixedUpdate()
    {
        ScaleChangeScript.ChangeOnWindow(transform, gameSettings.ScaleSettings.MinScaleOnWindow, gameSettings.ScaleSettings.MaxScaleOnWindow);

        ChageRadiusCollider(bombSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        blade.SubstractHealth(bombSettings.Damage);

        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        transform.GetChild(0).localScale = new Vector2(0, 0);

        ScaleChangeScript.Change(transform.GetChild(0), bombSettings.MaxScaleExplosion, bombSettings.TimeBeforeExplosion);

        DemonstrationPoints.Demonstration(gameObject,
            "¡ÛÏ!",
            gameSettings.TextMeshProSettings.TextPointsStyle,
            gameSettings.TextMeshProSettings,
            true,
            new Color(255, 255, 255)    
            );

        StopAllPhysicsEntity();

        yield return new WaitForSeconds(bombSettings.TimeBeforeExplosion);

        StartAllPhysics();
        CutSpriteScript.GetTwoHalves(gameObject.GetComponent<SpriteRenderer>().sprite.texture, gameObject);

        blade.Entitys.Remove(this);
        Destroy(gameObject);
    }

    protected override void StartAllPhysics()
    {
        SliceCheckScript.BlockSlice = false;

        for (int i = 0; i < blade.Entitys.Count; i++)
        {
            if (blade.Entitys[i] == null)
                continue;

            if (blade.Entitys[i] != this)
            {
                if (GetDistance(blade.Entitys[i].transform.position) <= gameSettings.BombSettings.ExplosionRadius)
                {
                    blade.Entitys[i].GetComponent<Physics>().TimeLive = 0;

                    blade.Entitys[i].Trow(GetAngel(blade.Entitys[i].transform.position),
                        bombSettings.CenterExplosionImpuls / GetDistance(blade.Entitys[i].transform.position),
                        gameSettings.Gravity,
                        blade.Entitys[i].transform.position);
                }
                else
                {
                    blade.Entitys[i].GetComponent<Physics>().Gravity = gravity[i];
                }
            }
        }

        foreach (SpawnerEntitys spawner in spawners)
        {
            if (spawner != null)
                spawner.gameObject.SetActive(true);
        }
    }

    private float GetAngel(Vector3 pos)
    {
        if(pos.y > transform.position.y)
        {
            return Vector3.Angle(pos, (pos - transform.position));
        }
        else
        {
            return 360 - Vector3.Angle(pos, (pos - transform.position));
        }

    }

    private float GetDistance(Vector2 pos)
    {
        return Mathf.Abs(Vector2.Distance(pos, transform.position));
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf && transform.position.y < WorldSizeCamera.HalfHeight)
        {
            Destroy(gameObject);
        }
    }
}
