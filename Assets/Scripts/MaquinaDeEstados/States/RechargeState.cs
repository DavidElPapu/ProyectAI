using UnityEngine;

[CreateAssetMenu(fileName = "NewRechargeState", menuName = "FSM/States/RechargeState")]
public class RechargeState : State
{
    [SerializeField] private float speed, energyRechargeInterval, energyRechargeAmount;
    private float energyRechargeIntervalCount;
    private GameObject chargingStation;
    private bool isCharging;

    private StateMachine myStateMachine;

    public override void EnterState(StateMachine stateMachine)
    {
        myStateMachine = stateMachine;
        isCharging = false;
        foreach (StateMachineData data in myStateMachine.context)
        {
            if (data.dataName == "ChargingStation")
            {
                chargingStation = data.objectData;
            }
        }
        foreach (StateMachineData data in myStateMachine.context)
        {
            if (data.dataName == "NavMeshAgent")
            {
                data.agentData.speed = speed;
                Vector3 adjustedChargePos = new Vector3(chargingStation.transform.position.x, myStateMachine.transform.position.y, chargingStation.transform.position.z);
                data.agentData.SetDestination(adjustedChargePos);
            }
        }
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (isCharging)
        {
            if (energyRechargeIntervalCount > 0)
            {
                energyRechargeIntervalCount -= Time.deltaTime;
                if (energyRechargeIntervalCount <= 0)
                {
                    energyRechargeIntervalCount = energyRechargeInterval;
                    GainEnergy();
                }
            }
        }
        else
        {
            if (IsCloseToChargeStation())
            {
                isCharging = true;
                energyRechargeIntervalCount = energyRechargeInterval;
            }
        }
    }

    private bool IsCloseToChargeStation()
    {
        if (Vector3.Distance(myStateMachine.transform.position, chargingStation.transform.position) <= 1f)
            return true;
        return false;
    }

    private void GainEnergy()
    {
        float maxEnergy = 0;
        foreach (StateMachineData data in myStateMachine.context)
        {
            if (data.dataName == "MaxEnergy")
            {
                maxEnergy = data.floatData;
            }
        }
        if (maxEnergy == 0)
        {
            Debug.Log("Falta Max Energy");
            return;
        }
        for (int i = 0; i < myStateMachine.context.Count; i++)
        {
            if (myStateMachine.context[i].dataName == "CurrentEnergy")
            {
                StateMachineData tempData = myStateMachine.context[i];
                tempData.floatData += energyRechargeAmount;
                if (tempData.floatData > maxEnergy)
                    tempData.floatData = maxEnergy;
                myStateMachine.context[i] = tempData;
            }
        }
    }
}
