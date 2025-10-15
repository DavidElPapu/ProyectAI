using UnityEngine;

[CreateAssetMenu(fileName = "NewIdleState", menuName = "FSM/States/IdleState")]
public class IdleState : State
{
    [SerializeField] private float trashDetectionRange, detectionInterval, energyConsumtionInterval, energyConsumptionAmount;
    private float detectionIntervalCount, energyConsumtionIntervalCount;
    [SerializeField] private string trashTag;
    [SerializeField] private LayerMask trashLayer;

    private StateMachine myStateMachine;

    public override void EnterState(StateMachine stateMachine)
    {
        myStateMachine = stateMachine;
        detectionIntervalCount = detectionInterval;
        energyConsumtionIntervalCount = energyConsumtionInterval;
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if(detectionIntervalCount > 0)
        {
            detectionIntervalCount -= Time.deltaTime;
            if (detectionIntervalCount <= 0)
            {
                detectionIntervalCount = detectionInterval;
                LookForTrash();
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

    private void ConsumeEnergy()
    {
        //foreach (StateMachineData data in myStateMachine.context)
        //{
        //    if (data.dataName == "CurrentEnergy")
        //    {
        //        //data.floatData = energyConsumptionAmount;
        //        StateMachineData tempData = data;
        //        tempData.floatData -= energyConsumptionAmount;
        //        data = tempData;
        //    }
        //}
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

    private void LookForTrash()
    {
        Collider[] maybeTrashInRange = Physics.OverlapSphere(myStateMachine.transform.position, trashDetectionRange, trashLayer);
        foreach (Collider maybeTrash in maybeTrashInRange)
        {
            if (maybeTrash.gameObject.CompareTag(trashTag))
            {
                //solo agarra la primera trash detectada
                for (int i = 0; i < myStateMachine.context.Count; i++)
                {
                    if (myStateMachine.context[i].dataName == "TargetPos")
                    {
                        StateMachineData tempData = myStateMachine.context[i];
                        tempData.objectData = maybeTrash.gameObject;
                        myStateMachine.context[i] = tempData;
                    }
                }
                return;
            }
        }
    }
}
