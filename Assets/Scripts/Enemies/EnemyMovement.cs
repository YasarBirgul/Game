using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement: MonoBehaviour
    {
        public Transform Player;
        public float UpdateSpeed;
        private NavMeshAgent _agent;
        [SerializeField]
        private Animator animator;
        private const string Walk = "Walk";
        private const string Idle = "Idle";
        private Coroutine FollowCoroutine;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            animator.SetBool(Walk,_agent.velocity.magnitude> 0.01f);
            animator.SetBool(Idle,_agent.velocity.magnitude == 0f);
        }

        public void StartChasing()
        {
            if (FollowCoroutine == null)
            {
               FollowCoroutine = StartCoroutine(FollowTarget());
            }
        }

        private IEnumerator FollowTarget()
        {
            WaitForSeconds wait = new WaitForSeconds(UpdateSpeed);

            while (gameObject.activeSelf)
            {
                _agent.SetDestination(Player.transform.position);
                yield return wait;
            }
        }
    }
}