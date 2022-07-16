using UnityEngine;
using System.Collections;

public class BombScript : Entity
{
    [SerializeField] private GameSettings gameSettings;

    private BombSettigns bombSettings;
    private SpawnerFruits[] spawners;

    private void Awake()
    {
        player = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerFruits>(); 

        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script");
    }

    private void Start()
    {
        player = FindObjectOfType<GamePlayEvents>();
        player.Entitys.Add(this);

        bombSettings = gameSettings.BombSettings;
    }

    private void FixedUpdate()
    {
        ScaleChangeScript.ChangeOnWindow(transform);

        ChageRadius();
    }

    private void ChageRadius()
    {
        if (bombSettings.RadiusCollider == 0)
            RadiusCollider = (transform.localScale.y / GetComponent<SpriteRenderer>().bounds.size.y) * 2f;
        else
            RadiusCollider = bombSettings.RadiusCollider;
    }

    public override void Destruction()
    {
        player.SubstractHealth(bombSettings.Damage);

        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        SliceCheckScript.BlockSlice = true;
        transform.GetChild(0).localScale = new Vector2(0, 0);

        ScaleChangeScript.Change(transform.GetChild(0), bombSettings.MaxScaleExplosion, bombSettings.TimeBeforeExplosion);

        int countEntitys = player.Entitys.Count;

        float[] impuls = new float[countEntitys];
        float[] gravity = new float[countEntitys];


        for(int i = 0; i < countEntitys; i++)
        {
            if (player.Entitys[i] == null)
                continue;

            var physics = player.Entitys[i].GetComponent<Physics>();

            impuls[i] = physics.Impuls;
            gravity[i] = physics.Gravity;

            physics.Impuls = 0;
            physics.Gravity = 0;
        }

        foreach(SpawnerFruits spawner in spawners)
        {
            spawner.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(bombSettings.TimeBeforeExplosion);
        SliceCheckScript.BlockSlice = false;

        for (int i = 0; i < countEntitys; i++)
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
                    player.Entitys[i].GetComponent<Physics>().Impuls = impuls[i];
                    player.Entitys[i].GetComponent<Physics>().Gravity = gravity[i];
                }
            }
        }

        foreach (SpawnerFruits spawner in spawners)
        {
            if(spawner != null)
                spawner.gameObject.SetActive(true);
        }

        player.Entitys.Remove(this);
        Destroy(gameObject);
    }

    private float GetAngel(Vector3 pos)
    {
        return Vector3.Angle (pos, (pos - transform.position));
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
