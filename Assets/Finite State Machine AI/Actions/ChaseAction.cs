using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine AI/Actions/Chase")]
public class ChaseAction : FSMAction
{
    Transform target;
    Movement mov;

    public override void Excute(BaseStateMachine machine)
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;

        mov = machine.GetComponent<Movement>();

        Vector3 dir = (target.position - machine.transform.position).normalized;

        mov.MoveDir(new Vector2(dir.x, dir.z));
    }
}