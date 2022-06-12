using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosion;

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
        if(TryGetComponent(out Collider col))
        {
            col.isTrigger = false;
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject, .3f);
    }
}
