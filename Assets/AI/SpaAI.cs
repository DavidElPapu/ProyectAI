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
        //Percepción
        float distance = Vector3.Distance(transform.position, player.position);

        //Planeación
        if (distance < fleeRange)
        {
            //Acción A
            Vector3 dir = (transform.position - player.position).normalized;
            Vector3 fleePos = transform.position + dir * 5f;
            agent.SetDestination(fleePos);
        }
        else
        {
            //Acción B
            agent.SetDestination(player.position);
        }
    }
}
