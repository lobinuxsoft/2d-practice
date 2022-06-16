using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine AI/Actions/Patrol")]
public class PatrolAction : FSMAction
{
    [SerializeField] float rayRadius = .45f;
    [SerializeField] float rayDistance = .5f;
    [SerializeField] Vector3 rayPosOffset = Vector3.zero;
    [SerializeField] LayerMask blockMask;

    private Vector2 dir = Vector2.up;

    public override void Excute(BaseStateMachine machine)
    {
        Movement mov = machine.GetComponent<Movement>();

        if(CheckDirToMove(machine.transform.position, new Vector3(dir.x, 0, -dir.y)))
        {
            dir = ChangeDir();
        }

        mov.MoveDir(dir);
    }

    private Vector2 ChangeDir()
    {
        Random.InitState(Mathf.RoundToInt(Time.time));

        int rnd = Random.Range(0, 4);

        switch(rnd)
        {
            case 0:
                return Vector2.up;
                
            case 1:
                return Vector2.right;

            case 2:
                return Vector2.down;

            default:
                return Vector2.left;
        }
    }

    private bool CheckDirToMove(Vector3 pos, Vector3 dir)
    {
        RaycastHit hit;
        Physics.SphereCast(pos + rayPosOffset, rayRadius, dir, out hit, rayDistance, blockMask);
        Debug.DrawRay(pos + rayPosOffset, dir * rayDistance, hit.collider ? Color.green : Color.red, 2);

        if (!hit.collider) return true;

        return false;
    }
}