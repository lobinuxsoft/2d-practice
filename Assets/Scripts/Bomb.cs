using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] LayerMask blockMask;
    [SerializeField] int explosionLenght = 3;
    [SerializeField] int delayExplosionsInMilisec = 50;
    [SerializeField] GameObject explosion;

    public int ExplosionLenght
    {
        get => explosionLenght;
        set => explosionLenght = value;
    }

    BlinkEffect blinkEffect;

    private void Awake() => blinkEffect = GetComponent<BlinkEffect>();

    // Start is called before the first frame update
    async void Start()
    {
        await blinkEffect.BlinkTask(3);

        Explode();
    }

    private void OnTriggerExit(Collider other)
    {
        if(TryGetComponent(out Collider col)) col.isTrigger = false;
    }

    async void Explode()
    {
        if (TryGetComponent(out Renderer renderer)) renderer.enabled = false;

        if(TryGetComponent(out Collider collider)) collider.enabled = false;


        Instantiate(explosion, transform.position, Quaternion.identity);

        List<Task> explosions = new List<Task>();

        explosions.Add(CreateExplosions(Vector3.forward, explosionLenght));
        explosions.Add(CreateExplosions(Vector3.right, explosionLenght));
        explosions.Add(CreateExplosions(Vector3.back, explosionLenght));
        explosions.Add(CreateExplosions(Vector3.left, explosionLenght));

        await Task.WhenAll(explosions);

        Destroy(gameObject);
    }

    async Task CreateExplosions(Vector3 direction, int amount)
    {
        for (int i = 1; i < amount; i++)
        {
            RaycastHit hit;
            Physics.SphereCast(transform.position + new Vector3(0, .5f, 0), .25f, direction, out hit, i, blockMask);

            if (!hit.collider)
                Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);
            else
                break;

            await Task.Delay(delayExplosionsInMilisec);
        }
    }
}