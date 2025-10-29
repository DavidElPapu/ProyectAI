using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace BehaviourTrees
{
    public interface IStrategy
    {
        Node.Status Process();
        void Reset();
    }

    public class PatrolStrategy : IStrategy
    {
        public Transform entity;
        public NavMeshAgent agent;
        public List<Transform> patrolPoints;
        public float patrolSpeed;
        public int currentIndex;

        private bool isPathCalculated;

        public PatrolStrategy(Transform entity, NavMeshAgent agent, List<Transform> patrolPoints, float patrolSpeed)
        {
            this.entity = entity;
            this.agent = agent;
            this.patrolPoints = patrolPoints;
            this.patrolSpeed = patrolSpeed;
        }

        public Node.Status Process()
        {
            if (currentIndex == patrolPoints.Count)
            {
                return Node.Status.Success;
            }

            Transform target = patrolPoints[currentIndex];
            agent.SetDestination(target.position);
            entity.LookAt(new Vector3(target.position.x, entity.position.y, target.position.z));

            if (isPathCalculated && agent.remainingDistance < 0.1f)
            {
                isPathCalculated = false;
                currentIndex++;
            }
            if (agent.pathPending)
            {
                isPathCalculated = true;
            }

            return Node.Status.Running;
        }

        public void Reset() => currentIndex = 0;
    }
}
