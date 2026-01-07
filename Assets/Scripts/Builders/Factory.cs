using UnityEngine;
using static Factory;

public class Factory : MonoBehaviour
{
    Enemy elCochiloco = EnemyFactory.Create(EnemyType.AlbertEpstein);
}

public abstract class Enemy
{
    public int Health;
    public abstract void Attack();
}

public class Zombie : Enemy
{
    public Zombie()
    {
        Health = 50;
    }

    public override void Attack()
    {
        Debug.Log("Zombie bite");
    }
}

public class SpukiSkariSkeleton : Enemy
{
    public SpukiSkariSkeleton()
    {
        Health = int.MaxValue;
    }

    public override void Attack()
    {
        Debug.Log("AAAAAAAAH");
    }
}

public class AlbertEpstein : Enemy
{
    public AlbertEpstein()
    {
        Health = 69;
    }

    public override void Attack()
    {
        Debug.Log("Files");
    }
}

public enum EnemyType
{
    Zombie,
    SpukiSkariSkeleton,
    AlbertEpstein
}

public static class EnemyFactory
{
    public static Enemy Create(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Zombie:
                return new Zombie();
            case EnemyType.SpukiSkariSkeleton:
                return new SpukiSkariSkeleton();
            case EnemyType.AlbertEpstein:
                return new AlbertEpstein();
            default:
                Debug.Log("Quien sos?");
                return null;
        }
    }
}
