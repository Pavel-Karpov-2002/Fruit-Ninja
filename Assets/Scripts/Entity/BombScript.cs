using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BombScript : Unit
{
    [SerializeField] private CutSpriteScript cutObject;
    [SerializeField] private TextInScreen boomTextInScreen;
    [SerializeField] private Transform explosionSprites;

    private BombSettigns _bombSettigns;
    private Sequence _sequence;

    private void Awake()
    {
        _sequence = DOTween.Sequence();

        StartCoroutine(OutOfBounds());
        _bombSettigns = Settings.BombSettings;

        PullObjects.Units.Add(this);
    }

    private void FixedUpdate()
    {
        ChangeScaleOnWindow();

        ChageRadiusCollider(Settings.BombSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        PullObjects.GamePlayer.SubstractHealth(Settings.BombSettings.Damage);
        PullObjects.Units.Remove(this);

        Explosion();
    }

    private void Explosion()
    {
        explosionSprites.localScale = new Vector2(0, 0);


        SearchNear(PullObjects.Units.ToArray());
        SearchNear(PullObjects.Halves.ToArray());
        
        _sequence = DOTween.Sequence();

        _sequence.Append(explosionSprites.DOScale(_bombSettigns.MaxScaleExplosion, _bombSettigns.TimeExplosion).SetEase(Ease.Linear));

        TextInScreen boom = Instantiate(boomTextInScreen);

        boom.Demonstration(SourceSprite,
            "Бум!",
            true,
            new Color(255, 255, 255)
            );

        StartCoroutine(DestroyBomb());
    }

    private void SearchNear(Entity[] entities)
    {
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i] == null)
                continue;

            if (entities[i] != this)
            {
                if (GetDistance(entities[i].transform.position) <= _bombSettigns.ExplosionRadius)
                {
                    AddExplosionImpuls(entities[i]);
                }
            }
        }
    }

    private void AddExplosionImpuls(Entity entity)
    {
        
        float percentageOfDistance = 100 / (_bombSettigns.ExplosionRadius / (GetDistance(entity.transform.position) / 2));

        entity.SourcePhysics.TimeLive = 0;
        entity.Trow(GetAngel(entity.transform.position),
            _bombSettigns.CenterExplosionImpuls * ( 1 - (percentageOfDistance / 100)),
            Settings.Gravity,
            entity.transform.position);
    }

    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(_bombSettigns.TimeExplosion);

        cutObject.CreateTwoHalves(SourcePhysics.Angle, SourcePhysics.Impuls, SourcePhysics.TimeLive, transform.position);

        Destroy(gameObject);
    }

    private float GetAngel(Vector3 pos)
    {
        if (pos.y > transform.position.y)
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
            PullObjects.Units.Remove(this);
            _sequence.Kill();
            Destroy(gameObject);
        }
    }
}
