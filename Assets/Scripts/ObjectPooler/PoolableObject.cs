using Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace ObjectPooler
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool Parent;
        public virtual void OnDisable()
        {
            Parent.ReturnObjectToPool(this);
        }
    }
}