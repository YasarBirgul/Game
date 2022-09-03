using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement: MonoBehaviour
    {
        public Transform Target;
        public float UpdateSpeed;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            StartCoroutine(FollowTarget());
        }

        private IEnumerator FollowTarget()
        {
            WaitForSeconds wait = new WaitForSeconds(UpdateSpeed);

            while (enabled)
            {
                _agent.SetDestination(Target.transform.position);
                yield return wait;
            }
        }
    }
}