using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;
using Enemies;
using UnityEngine.AI;

namespace General
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform Player;
        public int NumberOfEnemiesToSpawm;
        public float SpawnDelay = 1f;
        public List<Enemy> EnemyPrefabs = new List<Enemy>();
        public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;
    
        private Dictionary<int, ObjectPool.ObjectPool> EnemyObjectPool = new Dictionary<int, ObjectPool.ObjectPool>(); 
        private void Awake()
        {
            for (int i = 0; i < EnemyPrefabs.Count; i++)
            {
                 EnemyObjectPool.Add(i,ObjectPool.ObjectPool.CreateInstance(EnemyPrefabs[i],NumberOfEnemiesToSpawm));            
            }   
        } 
        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
            int SpawnEnemies = 0;

            while (SpawnEnemies < NumberOfEnemiesToSpawm)
            {
                if (EnemySpawnMethod == SpawnMethod.RoundRobin)
                {
                    SpawnRoundRobinEnemy(SpawnEnemies);
                }
                else if (EnemySpawnMethod == SpawnMethod.Random)
                {
                    
                }
                SpawnEnemies++;
                yield return wait;
            }
        }
        private void SpawnRoundRobinEnemy(int SpawnedEnemy)
        {
            int SpawnIndex = SpawnedEnemy % EnemyPrefabs.Count;
            
            DoSpawnEnemy(SpawnedEnemy);
        }
        private void SpawnRandomEnemy(int SpawnIndex)
        {
            DoSpawnEnemy(Random.Range(0,EnemyPrefabs.Count));
        }

        private void DoSpawnEnemy(int SpawnIndex)
        {
            PoolableObject poolableObject = EnemyObjectPool[SpawnIndex].GetObject();

            if (poolableObject != null)
            {
                Enemy enemy = poolableObject.GetComponent<Enemy>();
                NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

            }
            else
            {
                
            }
        }
        public enum SpawnMethod
         {
            RoundRobin,
            Random,
         }
    }
}

