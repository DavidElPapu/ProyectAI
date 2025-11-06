using UnityEngine;
using UnityEngine.AI;

public class PetStateMachine : StateMachine
{
    private void Awake()
    {
        blackboard.Set(Pet_BBKeys.NavMeshAgent, gameObject.GetComponent<NavMeshAgent>());
        blackboard.Set(Pet_BBKeys.HomeTransform, GameObject.Find("FSMPetHome").transform);
        blackboard.Set(Pet_BBKeys.OwnerGO, GameObject.FindWithTag("Player"));
        blackboard.Set(Pet_BBKeys.OwnerDetectionRange, 10f);
        blackboard.Set(Pet_BBKeys.IsOwnerFar, false);
    }
}

public static class Pet_BBKeys
{
    public const int HomeTransform = 0;
    public const int NavMeshAgent = 1;
    public const int OwnerDetectionRange = 2;
    public const int OwnerGO = 3;
    public const int IsOwnerFar = 4;
}
