using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement: MonoBehaviour
    {
        public Transform Target;
        public float UpdateSpeed;
        private NavMeshAgent _agent;
        [SerializeField]
        private Animator animator;
        private const string Walk = "Walk";

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            
        }

        private void Update()
        {
            animator.SetBool(Walk,_agent.velocity.magnitude> 0.01f);
            animator.SetBool(Walk,_agent.velocity.magnitude !> 0.01f);
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