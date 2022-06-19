using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Finite State Machine AI/Actions/Chase")]
public class ChaseAction : FSMAction
{
    NavMeshAgent agent;

    public override void Excute(BaseStateMachine machine)
    {
        agent = machine.GetComponent<NavMeshAgent>();
        Vector3 dir = (InLineOfSightDecision.lastPos - machine.transform.position).normalized;
        agent.SetDestination(machine.transform.position + dir);
    }
}