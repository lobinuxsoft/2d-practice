using UnityEngine;

public abstract class FSMAction : ScriptableObject
{
    public abstract void Excute(BaseStateMachine machine);
}