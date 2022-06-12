using UnityEngine;
using UnityEngine.InputSystem;

public class BombDropper : MonoBehaviour
{
    [SerializeField] float highSpawnOffset = 0;
    [SerializeField] int explosionLenght = 3;
    [SerializeField] Bomb bombPrefab;

    public void InputDropBomb(InputAction.CallbackContext context)
    {
        if (context.performed == false) return;
        
        DropBomb();
    }

    public void DropBomb()
    {
        if(bombPrefab)
        {
            Vector3 pos = new Vector3(Mathf.RoundToInt(transform.position.x), highSpawnOffset, Mathf.RoundToInt(transform.position.z));
            bombPrefab.ExplosionLenght = explosionLenght;
            Instantiate(bombPrefab, pos, bombPrefab.transform.rotation);
        }
    }
}
