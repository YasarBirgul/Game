using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPooler;
using UnityEngine;
using Enemies;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace General
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform Player;
        public int NumberOfEnemiesToSpawm;
        public float SpawnDelay = 1f;
        public List<Enemy> EnemyPrefabs = new List<Enemy>();
        public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;
        private NavMeshTriangulation _triangulation;
    
        private Dictionary<int, ObjectPool> EnemyObjectPool = new Dictionary<int, ObjectPool>(); 
        private void Awake()
        {
            for (int i = 0; i < EnemyPrefabs.Count; i++)
            {
                 EnemyObjectPool.Add(i,ObjectPool.CreateInstance(EnemyPrefabs[i],NumberOfEnemiesToSpawm));            
            }   
        } 
        private void Start()
        { 
            _triangulation = NavMesh.CalculateTriangulation();
            StartCoroutine(SpawnEnemies());
        }
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
            int SpawnedEnemies = 0;

            while (SpawnedEnemies < NumberOfEnemiesToSpawm)
            {
                if (EnemySpawnMethod == SpawnMethod.RoundRobin)
                {
                    SpawnRoundRobinEnemy(SpawnedEnemies);
                }
                else if (EnemySpawnMethod == SpawnMethod.Random)
                {
                    SpawnRandomEnemy();
                }
                SpawnedEnemies++;
                yield return wait;
            }
        }
        private void SpawnRoundRobinEnemy(int SpawnedEnemies)
        {
            int SpawnIndex = SpawnedEnemies % EnemyPrefabs.Count;
            
            DoSpawnEnemy(SpawnIndex);
        }
        private void SpawnRandomEnemy()
        {
            DoSpawnEnemy(Random.Range(0,EnemyPrefabs.Count));
        }

        private void DoSpawnEnemy(int SpawnIndex)
        {
            PoolableObject poolableObject = EnemyObjectPool[SpawnIndex].GetObject();

            if (poolableObject != null)
            {
                Enemy enemy = poolableObject.GetComponent<Enemy>();
                int VertexIndex = Random.Range(0, _triangulation.vertices.Length);
                NavMeshHit Hit;

                if (NavMesh.SamplePosition(_triangulation.vertices[VertexIndex], out Hit, 2f, 0))
                {
                    enemy.Agent.Warp(Hit.position);
                    enemy.Movement.Player = Player;
                    enemy.Agent.enabled = true;
                    enemy.Movement.StartChasing();
                }
            }
        }
        public enum SpawnMethod
         {
            RoundRobin,
            Random,
         }
    }
}

