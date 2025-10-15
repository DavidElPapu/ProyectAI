using UnityEngine;

[CreateAssetMenu(fileName = "NewGoToPlaceState", menuName = "FSM/States/GoToPlaceState")]
public class GoToPlaceState : State
{
    [SerializeField] private float speed;
    [SerializeField] private string placeName;
    private GameObject placeObject;
    private bool isOnPlace;

    private StateMachine myStateMachine;


    public override void EnterState(StateMachine stateMachine)
    {
        myStateMachine = stateMachine;
        isOnPlace = false;
        foreach (StateMachineData data in myStateMachine.context)
        {
            if (data.dataName == placeName)
            {
                placeObject = data.objectData;
            }
        }
        foreach (StateMachineData data in myStateMachine.context)
        {
            if (data.dataName == "NavMeshAgent")
            {
                data.agentData.speed = speed;
                Vector3 adjustedChargePos = new Vector3(placeObject.transform.position.x, myStateMachine.transform.position.y, placeObject.transform.position.z);
                data.agentData.SetDestination(adjustedChargePos);
            }
        }
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (!isOnPlace)
        {
            if (IsCloseToPlace())
            {
                isOnPlace = true;
            }
        }
    }

    private bool IsCloseToPlace()
    {
        if (Vector3.Distance(myStateMachine.transform.position, placeObject.transform.position) <= 1f)
            return true;
        return false;
    }
}
