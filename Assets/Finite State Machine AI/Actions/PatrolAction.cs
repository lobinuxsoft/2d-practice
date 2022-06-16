using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Finite State Machine AI/Actions/Patrol")]
public class PatrolAction : FSMAction
{
    [SerializeField] float rayRadius = .45f;
    [SerializeField] float rayDistance = .5f;
    [SerializeField] Vector3 rayPosOffset = Vector3.zero;
    [SerializeField] LayerMask blockMask;

    private Vector3 dir = Vector3.forward;

    public override void Excute(BaseStateMachine machine)
    {
        NavMeshAgent agent = machine.GetComponent<NavMeshAgent>();

        if(CheckDirToMove(machine.transform.position, dir))
        {
            dir = ChangeDir();
        }

        agent.SetDestination(machine.transform.position + dir);
    }

    private Vector3 ChangeDir()
    {
        Random.InitState(Mathf.RoundToInt(Time.time));

        int rnd = Random.Range(0, 4);

        switch(rnd)
        {
            case 0:
                return Vector3.forward;
                
            case 1:
                return Vector3.right;

            case 2:
                return Vector3.back;

            default:
                return Vector3.left;
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