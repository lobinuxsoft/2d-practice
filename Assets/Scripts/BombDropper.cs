using UnityEngine;
using UnityEngine.InputSystem;

public class BombDropper : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;

    public void InputDropBomb(InputAction.CallbackContext context)
    {
        if (context.performed == false) return;
        
        DropBomb();
    }

    public void DropBomb()
    {
        if(bombPrefab)
        {
            Vector3 pos = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
            Instantiate(bombPrefab, pos, bombPrefab.transform.rotation);
        }
    }
}
