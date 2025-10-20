using UnityEngine;

public class EnemyBorrame : MonoBehaviour
{
    public GameObject ibObject;
    InterfazBorrame ib;

    private void Awake()
    {

        ibObject.TryGetComponent<InterfazBorrame>(out ib);
    }

    private void Start()
    {
        ShootWeapon();
    } 

    private void ShootWeapon()
    {
        ib.Shoot();
    }
}
