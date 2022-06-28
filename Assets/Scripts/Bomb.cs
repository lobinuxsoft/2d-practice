using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] LayerMask blockMask;
    [SerializeField] int explosionLenght = 3;
    [SerializeField] float delayExplosionsInSec = .025f;
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

        StartCoroutine(Explode());
    }

    private void OnTriggerExit(Collider other)
    {
        if(TryGetComponent(out Collider col)) col.isTrigger = false;
    }

    IEnumerator Explode()
    {
        if (TryGetComponent(out Renderer renderer)) renderer.enabled = false;

        if (TryGetComponent(out Collider collider)) collider.enabled = false;


        Instantiate(explosion, transform.position, Quaternion.identity);

        List<Coroutine> routines = new List<Coroutine>();

        routines.Add(StartCoroutine(CreateExplosions(Vector3.forward, explosionLenght)));
        routines.Add(StartCoroutine(CreateExplosions(Vector3.right, explosionLenght)));
        routines.Add(StartCoroutine(CreateExplosions(Vector3.back, explosionLenght)));
        routines.Add(StartCoroutine(CreateExplosions(Vector3.left, explosionLenght)));

        foreach (Coroutine routine in routines)
        {
            yield return routine;
        }

        Destroy(gameObject);
    }

    IEnumerator CreateExplosions(Vector3 direction, int amount)
    {
        for (int i = 1; i < amount; i++)
        {
            RaycastHit hit;
            Physics.SphereCast(transform.position + new Vector3(0, .5f, 0), .25f, direction, out hit, i, blockMask);

            if (!hit.collider)
                Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);
            else
                break;

            yield return new WaitForSeconds(delayExplosionsInSec);
        }
    }
}