using UnityEngine;
using System.Collections.Generic;

public class StockpileScript : MonoBehaviour
{
    [SerializeField]
    private GameObject woodPrefab;
    [SerializeField]
    private Transform dropSpawn;
    private List<GameObject> woodGOs = new List<GameObject>();

    public void DepositWood()
    {
        GameObject newWood = Instantiate(woodPrefab, dropSpawn.position, dropSpawn.rotation, transform);
        woodGOs.Add(newWood);
    }

    public bool CanPickWood()
    {
        if (woodGOs.Count > 0)
        {
            Destroy(woodGOs[0]);
            woodGOs.RemoveAt(0);
            return true;
        }
        return false;
    }
}
