using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine AI/Decisions/In Line Of Sight")]
public class InLineOfSightDecision : Decision
{
    [SerializeField] bool iSawIt = false;
    [SerializeField] float reachDistance = 1f;

    public static Vector3 lastPos = Vector3.zero;
    Transform target;

    public override bool Decide(BaseStateMachine machine)
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;

        RaycastHit hit;
        Physics.Linecast(machine.transform.position, target.position, out hit);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            iSawIt = true;
            lastPos = target.position;
        }

        if (iSawIt && Vector3.Distance(machine.transform.position, lastPos) < reachDistance) iSawIt = false;


        return iSawIt;
    }
}
