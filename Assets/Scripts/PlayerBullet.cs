using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.TryGetComponent(out MimicStatueAI enemyScript))
        {
            enemyScript.GetHit(10f);
            Destroy(gameObject);
        }
    }
}
