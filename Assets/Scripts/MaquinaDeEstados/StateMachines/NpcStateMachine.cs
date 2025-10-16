using UnityEngine;

public class NpcStateMachine : StateMachine
{
    protected override void Start()
    {
        blackboard.Set(NPC_BBKeys.HomeTransform, GameObject.FindGameObjectWithTag("Player"));
        ChangeState(initialState);
    }
}

public static class NPC_BBKeys
{
    public const int HomeTransform = 0;
}
