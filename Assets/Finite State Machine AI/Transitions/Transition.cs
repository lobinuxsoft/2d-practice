using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine AI/Transition")]
public class Transition : ScriptableObject
{
    [SerializeField] private Decision decision;
    [SerializeField] private BaseState trueState;
    [SerializeField] private BaseState falseState;

    public Decision Decision => decision;

    public BaseState TrueState => trueState;

    public BaseState FalseState => falseState;

    public void Excute(BaseStateMachine machine)
    {
        if (Decision.Decide(machine) && !(TrueState is RemainInState))
            machine.CurrentState = TrueState;
        else if (!(FalseState is RemainInState))
            machine.CurrentState = FalseState;
    }
}