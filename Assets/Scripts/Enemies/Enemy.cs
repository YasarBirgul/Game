using ObjectPooler;
using UnityEngine.AI;

namespace Enemies
{ 
    public class Enemy : PoolableObject
    {
        public EnemyMovement Movement;
        public NavMeshAgent Agent;
        public int Health = 100;

        public override void OnDisable()
        {
            base.OnDisable();

            Agent.enabled = false;
        }
    }
}