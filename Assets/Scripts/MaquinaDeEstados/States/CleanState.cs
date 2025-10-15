using UnityEngine;

[CreateAssetMenu(fileName = "NewCleanState", menuName = "FSM/States/CleanState")]
public class CleanState : State
{
    [SerializeField] private float speed, trashCleaningRange, trashCleaningInterval, energyConsumtionInterval, energyConsumptionAmount;
    [SerializeField] private string trashTag;
    [SerializeField] private LayerMask trashLayer;
    private float trashCleaningIntervalCount, energyConsumtionIntervalCount;
    private GameObject trashTarget;

    private StateMachine myStateMachine;

    public override void EnterState(StateMachine stateMachine)
    {
        myStateMachine = stateMachine;
        trashCleaningIntervalCount = trashCleaningInterval;
        energyConsumtionIntervalCount = energyConsumtionInterval;
        foreach (StateMachineData data in myStateMachine.context)
        {
            if (data.dataName == "TargetPos")
            {
                trashTarget = data.objectData;
            }
        }
        foreach (StateMachineData data in myStateMachine.context)
        {
            if (data.dataName == "NavMeshAgent")
            {
                data.agentData.speed = speed;
                Vector3 adjustedTrashPos = new Vector3(trashTarget.transform.position.x, myStateMachine.transform.position.y, trashTarget.transform.position.z);
                data.agentData.SetDestination(adjustedTrashPos);
            }
        }
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (trashCleaningIntervalCount > 0)
        {
            trashCleaningIntervalCount -= Time.deltaTime;
            if (trashCleaningIntervalCount <= 0)
            {
                trashCleaningIntervalCount = trashCleaningInterval;
                CleanClose();
            }
        }
        if (energyConsumtionIntervalCount > 0)
        {
            energyConsumtionIntervalCount -= Time.deltaTime;
            if (energyConsumtionIntervalCount <= 0)
            {
                energyConsumtionIntervalCount = energyConsumtionInterval;
                ConsumeEnergy();
            }
        }
    }

    private void CleanClose()
    {
        Collider[] maybeTrashInRange = Physics.OverlapSphere(myStateMachine.transform.position, trashCleaningRange, trashLayer);
        foreach (Collider maybeTrash in maybeTrashInRange)
        {
            if (maybeTrash.gameObject.CompareTag(trashTag))
            {
                if (maybeTrash == trashTarget)
                {
                    for (int i = 0; i < myStateMachine.context.Count; i++)
                    {
                        if (myStateMachine.context[i].dataName == "TargetPos")
                        {
                            StateMachineData tempData = myStateMachine.context[i];
                            tempData.objectData = null;
                            myStateMachine.context[i] = tempData;
                        }
                    }
                }
                Destroy(maybeTrash.gameObject);
            }
        }
    }

    private void ConsumeEnergy()
    {
        for (int i = 0; i < myStateMachine.context.Count; i++)
        {
            if (myStateMachine.context[i].dataName == "CurrentEnergy")
            {
                StateMachineData tempData = myStateMachine.context[i];
                tempData.floatData -= energyConsumptionAmount;
                myStateMachine.context[i] = tempData;
            }
        }
    }
}
