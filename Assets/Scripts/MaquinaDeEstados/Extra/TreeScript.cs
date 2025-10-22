using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool IsTreeChopped(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            return true;
        return false;
    }

    public int GetWoodAmount()
    {
        return maxHealth;
    }
}
