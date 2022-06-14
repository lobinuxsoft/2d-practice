using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine AI/Decisions/In Line Of Sight")]
public class InLineOfSightDecision : Decision
{
    Transform target;
    public override bool Decide(BaseStateMachine machine)
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;

        RaycastHit hit;
        Physics.Linecast(machine.transform.position, target.position, out hit);

        if (hit.collider == null) return false;

        return hit.collider.CompareTag("Player");
    }
}
