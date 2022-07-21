using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem _system;

    private void Start()
    {
        Destroy(gameObject, _system.startLifetime - 0.1f);
    }

}
