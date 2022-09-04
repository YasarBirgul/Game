using System;
using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;

namespace General
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Camera camera =null;
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent _agent;
        
        private RaycastHit[] Hits = new RaycastHit[1];
        private const string Walk = "Walk";
        private const string Jump = "Jump";
        private const string Idle = "Idle";
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        } 
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.RaycastNonAlloc(ray, Hits) > 0)
                {
                    _agent.SetDestination(Hits[0].point);
                }
            }
            animator.SetBool(Walk,_agent.velocity.magnitude > 0.01f);
            animator.SetBool(Idle,_agent.velocity.magnitude == 0f);
        }
    }
}