using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    protected virtual void Awake()
    {
        blackboard.Set(Enemy_BBKeys.Player, GameObject.FindWithTag(Enemy_BBKeys.PlayerTag));
        blackboard.Set(Enemy_BBKeys.NavMeshAgent, gameObject.GetComponent<NavMeshAgent>());
        blackboard.Set(Enemy_BBKeys.Speed, 5f);
        blackboard.Set(Enemy_BBKeys.Damage, 1f);
        blackboard.Set(Enemy_BBKeys.RunSpeed, 10f);
        blackboard.Set(Enemy_BBKeys.MaxHealth, 100f);
        blackboard.Set(Enemy_BBKeys.Health, 100f);
        blackboard.Set(Enemy_BBKeys.WonderDirectionChangeInterval, 1f);
    }
}

public static class Enemy_BBKeys
{
    public const string PlayerTag = "Player";
    public const int Player = 0;
    public const int NavMeshAgent = 1;
    public const int Speed = 2;
    public const int Damage = 3;
    public const int RunSpeed = 4;
    public const int MaxHealth = 5;
    public const int Health = 6;
    public const int WonderDirectionChangeInterval = 7;
}
