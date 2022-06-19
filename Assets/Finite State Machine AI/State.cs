using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine AI/State")]
public sealed class State : BaseState
{
    public List<FSMAction> actions = new List<FSMAction>();
    public List<Transition> transitions = new List<Transition>();

    public override void Execute(BaseStateMachine machine)
    {
        foreach (var action in actions)
        {
            action.Excute(machine);
        }

        foreach (var transition in transitions)
        {
            transition.Excute(machine);
        }
    }
}