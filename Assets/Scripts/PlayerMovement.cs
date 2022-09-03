using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        private NavMeshAgent _agent;
        private RaycastHit[] Hits = new RaycastHit[1];
        
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
        }
    }
}