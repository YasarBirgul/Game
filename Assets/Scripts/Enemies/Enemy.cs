using System;
using ObjectPool;
using UnityEngine.AI;

namespace Enemies
{ 
    [Serializable]
    public class Enemy : PoolableObject
    {
        public EnemyMovement Movement;
        public NavMeshAgent Agent;
        public int Health = 100;
    }
}