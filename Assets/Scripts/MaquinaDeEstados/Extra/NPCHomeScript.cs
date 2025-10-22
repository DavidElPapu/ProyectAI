using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class NPCHomeScript : MonoBehaviour
{
    public Transform homeEntrance, homeInside;
    [SerializeField]
    private List<GameObject> homeStagesModels = new List<GameObject>();
    [SerializeField]
    private List<int> requiredWoodPerStage = new List<int>();
    [SerializeField]
    private GameObject spawnedNPCPrefab;
    [SerializeField]
    private int completedStageIndex, currentStage;

    private int currentWoodProgress;


    private void Awake()
    {
        currentWoodProgress = 0;
        if (currentStage == completedStageIndex)
            OnCompleted();
    }

    public bool CanBuildHere()
    {
        if (currentStage >= (homeStagesModels.Count - 1))
            return false;
        return true;
    }

    public int GetHomeLevel()
    {
        return currentStage - completedStageIndex;
    }

    public void AddWood(int woodAmount)
    {
        currentWoodProgress += woodAmount;
        if (currentWoodProgress >= requiredWoodPerStage[currentStage + 1])
            OnBuildStageChange();
    }

    private void OnBuildStageChange()
    {
        currentStage++;
        currentWoodProgress -= requiredWoodPerStage[currentStage];
        if (currentStage >= homeStagesModels.Count)
        {
            Debug.Log("La casa ya esta al maximo, algo salio mal porque no se deberia mejorar aqui");
            return;
        }
        else
        {
            homeStagesModels[currentStage - 1].SetActive(false);
            homeStagesModels[currentStage].SetActive(true);
            //NavMeshObstacle newNMO = gameObject.AddComponent<NavMeshObstacle>();
            //newNMO.size = new Vector3(5, 5, 5);
            //newNMO.center = new Vector3(0, 2.5f, 0);
            //newNMO.carving = true;
            if (currentStage == completedStageIndex)
                OnCompleted();
        }
    }

    private void OnCompleted()
    {
        GameObject newNPC = Instantiate(spawnedNPCPrefab, homeInside.position, homeInside.rotation);
        if (newNPC.TryGetComponent(out StateMachine npcSM))
            npcSM.blackboard.Set(LnBNPC_BBKeys.HomeScript, this);
        else
            Debug.Log("El NPC no tiene StateMachine");
    }
}
