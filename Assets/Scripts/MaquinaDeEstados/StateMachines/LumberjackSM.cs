using UnityEngine;

public class LumberjackSM : StateMachine
{
    private void Awake()
    {
        GameObject objectsParent = FindAnyObjectByType<ObjectParentHelper>().gameObject;
        if (objectsParent == null)
        {
            Debug.Log("Falta un objeto padre en la escena");
            return;
        }
        blackboard.Set(Lumberjack_BBKeys.HomeTransform, objectsParent.transform.Find("LumberjackHome").transform);
        blackboard.Set(Lumberjack_BBKeys.BreakfastTableTransform, objectsParent.transform.Find("BreakfastTable").transform);
        blackboard.Set(Lumberjack_BBKeys.StockpileGO, objectsParent.transform.Find("Stockpile"));
        blackboard.Set(Lumberjack_BBKeys.AxeSwingSpeed, 0.9f);
        blackboard.Set(Lumberjack_BBKeys.AxeForce, 1);
        blackboard.Set(Lumberjack_BBKeys.WoodInInventory, 0);
        blackboard.Set(Lumberjack_BBKeys.InventoryCapacity, 6);
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log(blackboard.Get<ObjectParentHelper>(Lumberjack_BBKeys.HomeTransform).gameObject.name);
    }
}

public static class Lumberjack_BBKeys
{
    public const int HomeTransform = 0;
    public const int BreakfastTableTransform = 1;
    public const int StockpileGO = 2;
    public const int AxeSwingSpeed = 3;
    public const int WoodInInventory = 4;
    public const int InventoryCapacity = 5;
    public const int AxeForce = 6;
}
