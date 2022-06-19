using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] int enemiesAmount = 0;
    [SerializeField] int blocksAmount = 0;
    [SerializeField] GameObject doorPrefab;

    GameObject playerInScene = null;
    GameObject doorInScene = null;

    public UnityEvent onLevelCompleted;

    // Start is called before the first frame update
    void Start()
    {
        playerInScene = GameObject.FindGameObjectWithTag("Player");

        var allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemiesAmount = allEnemies.Length;

        foreach (var enemy in allEnemies)
        {
            if(enemy.TryGetComponent(out Damageable damageable))
            {
                damageable.onDestroy.AddListener(EnemyDestroy);
            }
        }

        var allBlocks = GameObject.FindGameObjectsWithTag("Block");

        blocksAmount = allBlocks.Length;

        foreach (var block in allBlocks)
        {
            if(block.TryGetComponent(out Damageable damageable))
            {
                damageable.onDestroy.AddListener(BlockDestroy);
            }
        }
    }

    private void EnemyDestroy(Vector3 pos)
    {
        enemiesAmount--;

        if (doorInScene) OpenDoor(enemiesAmount <= 0);
    }

    private void BlockDestroy(Vector3 pos)
    {
        blocksAmount--;

        if (!doorInScene)
        {
            if(blocksAmount > 0)
            {
                Random.InitState(Mathf.RoundToInt(Time.time));

                int rnd = Random.Range(0, blocksAmount);

                if(rnd > blocksAmount * .9f)
                {
                    doorInScene = Instantiate(doorPrefab, pos, Quaternion.identity);

                    if (doorInScene.TryGetComponent(out SphereEventTrigger eventTrigger))
                    {
                        eventTrigger.onTriggerEnterEvent.AddListener(LevelCompleted);
                    }

                    OpenDoor(enemiesAmount <= 0);
                }
            }
            else
            {
                doorInScene = Instantiate(doorPrefab, pos, Quaternion.identity);

                if (doorInScene.TryGetComponent(out SphereEventTrigger eventTrigger))
                {
                    eventTrigger.onTriggerEnterEvent.AddListener(LevelCompleted);
                }

                OpenDoor(enemiesAmount <= 0);
            }
        }
    }

    private void OpenDoor(bool open)
    {
        if (doorInScene.TryGetComponent(out SphereEventTrigger eventTrigger))
        {
            eventTrigger.enabled = open;
        }

        if (doorInScene.TryGetComponent(out ParticleSystem particle))
        {
            var mainModule = particle.main;
            mainModule.startColor = open ? Color.cyan : Color.red;
        }
    }

    private void LevelCompleted(GameObject obj)
    {
        if (obj.CompareTag("Player"))
        {
            playerInScene.SetActive(false);

            if(doorInScene.TryGetComponent(out ParticleSystem particle))
            {
                particle.Stop();
            }

            onLevelCompleted?.Invoke();
        }
    }
}
