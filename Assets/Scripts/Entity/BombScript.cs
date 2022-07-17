using UnityEngine;
using System.Collections;

public class BombScript : Entity
{
    [SerializeField] private GameSettings gameSettings;

    private BombSettigns bombSettings;

    private void Awake()
    {
        player = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerEntitys>(); 

        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script");
    }

    private void Start()
    {
        heightSprite = (GetComponent<SpriteRenderer>().sprite.bounds.size.y) / 2;
        player = FindObjectOfType<GamePlayEvents>();
        player.Entitys.Add(this);

        bombSettings = gameSettings.BombSettings;
    }

    private void FixedUpdate()
    {
        ScaleChangeScript.ChangeOnWindow(transform, gameSettings.ScaleSettings.MinScaleOnWindow, gameSettings.ScaleSettings.MaxScaleOnWindow);

        ChageRadiusCollider(bombSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        player.SubstractHealth(bombSettings.Damage);

        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        transform.GetChild(0).localScale = new Vector2(0, 0);

        ScaleChangeScript.Change(transform.GetChild(0), bombSettings.MaxScaleExplosion, bombSettings.TimeBeforeExplosion);


        StopAllPhysicsEntity();

        yield return new WaitForSeconds(bombSettings.TimeBeforeExplosion);

        StartAllPhysics();

        player.Entitys.Remove(this);
        Destroy(gameObject);
    }

    protected override void StartAllPhysics()
    {
        SliceCheckScript.BlockSlice = false;

        for (int i = 0; i < player.Entitys.Count; i++)
        {
            if (player.Entitys[i] == null)
                continue;

            if (player.Entitys[i] != this)
            {
                if (GetDistance(player.Entitys[i].transform.position) <= gameSettings.BombSettings.ExplosionRadius)
                {
                    player.Entitys[i].GetComponent<Physics>().TimeLive = 0;

                    player.Entitys[i].Trow(GetAngel(player.Entitys[i].transform.position),
                        bombSettings.CenterExplosionImpuls / GetDistance(player.Entitys[i].transform.position),
                        gameSettings.Gravity,
                        player.Entitys[i].transform.position);
                }
                else
                {
                    player.Entitys[i].GetComponent<Physics>().Gravity = gravity[i];
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
