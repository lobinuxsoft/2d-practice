using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine AI/Actions/Patrol")]
public class PatrolAction : FSMAction
{
    [SerializeField] LayerMask blockMask;

    public override void Excute(BaseStateMachine machine)
    {
        Movement mov = machine.GetComponent<Movement>();

        if(CheckDirToMove(machine.transform.position, Vector3.forward))
        {
            mov.MoveDir(Vector2.up);
        }
        else if (CheckDirToMove(machine.transform.position, Vector3.right))
        {
            mov.MoveDir(Vector2.right);
        }
        else if (CheckDirToMove(machine.transform.position, Vector3.back))
        {
            mov.MoveDir(Vector2.down);
        }
        else if (CheckDirToMove(machine.transform.position, Vector3.left))
        {
            mov.MoveDir(Vector2.left);
        }
    }

    private bool CheckDirToMove(Vector3 pos, Vector3 dir)
    {
        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;
            Physics.SphereCast(pos + new Vector3(0, .5f, 0), .25f, dir, out hit, i, blockMask);

            if (!hit.collider) return true;
        }

        return false;
    }
}