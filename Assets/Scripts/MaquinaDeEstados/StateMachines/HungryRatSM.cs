using UnityEngine;
using UnityEngine.AI;

public class HungryRatSM : EnemyStateMachine
{
    protected override void Awake()
    {
        blackboard.Set(Enemy_BBKeys.Player, GameObject.FindWithTag(Enemy_BBKeys.PlayerTag));
        blackboard.Set(Enemy_BBKeys.NavMeshAgent, gameObject.GetComponent<NavMeshAgent>());
        blackboard.Set(Enemy_BBKeys.Speed, 6f);
        blackboard.Set(Enemy_BBKeys.Damage, 0f);
        blackboard.Set(Enemy_BBKeys.RunSpeed, 10f);
        blackboard.Set(Enemy_BBKeys.MaxHealth, 10f);
        blackboard.Set(Enemy_BBKeys.Health, 10f);
        blackboard.Set(Enemy_BBKeys.WonderDirectionChangeInterval, 1f);
        blackboard.Set(EnemyRat_BBKeys.Food, GameObject.FindWithTag(EnemyRat_BBKeys.FoodTag));
        blackboard.Set(EnemyRat_BBKeys.EatTime, 3f);
    }
}

public static class EnemyRat_BBKeys
{
    public const string FoodTag = "Food";
    public const int Food = 0;
    public const int EatTime = 1;
}
