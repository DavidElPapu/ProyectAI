using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Morgana : MonoBehaviour
    {
        public BehaviourTree tree;
        public GameObject prize;
        public List<Transform> patrolPoints = new List<Transform>();

        private NavMeshAgent agent;

        private void Awake()
        {
            tree = new BehaviourTree("Le che");

            agent = GetComponent<NavMeshAgent>();

            Condition cond = new Condition(() => prize.activeSelf);
            Leaf isPrizePresent = new Leaf("IsPrizePresent", cond);

            ActionStrategy actStrat = new ActionStrategy(() => agent.SetDestination(prize.transform.position));
            Leaf moveToPrize = new Leaf("MoveToPrize", actStrat);

            Sequence findPrize = new Sequence("FindPrize");
            findPrize.AddChild(isPrizePresent);
            findPrize.AddChild(moveToPrize);

            Selector baseSelector = new Selector("Base Selector");
            baseSelector.AddChild(findPrize);
            baseSelector.AddChild(new Leaf("Patrol", new PatrolStrategy(transform, agent, patrolPoints, 3f)));

            tree.AddChild(baseSelector);
        }

        private void Update()
        {
            tree.Process();
        }
    }
}
