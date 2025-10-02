using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SpaAI : MonoBehaviour
{
    public Transform player;
    public float fleeRange = 3f;
    private NavMeshAgent agent;

    private void Start()
    {
        gameObject.TryGetComponent(out agent);
    }
    private void Update()
    {
        //Percepci�n
        float distance = Vector3.Distance(transform.position, player.position);

        //Planeaci�n
        if (distance < fleeRange)
        {
            //Acci�n A
            Vector3 dir = (transform.position - player.position).normalized;
            Vector3 fleePos = transform.position + dir * 5f;
            agent.SetDestination(fleePos);
        }
        else
        {
            //Acci�n B
            agent.SetDestination(player.position);
        }
    }
}
