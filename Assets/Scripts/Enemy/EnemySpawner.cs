using System;
using Cysharp.Threading.Tasks;
using ScriptableObjects;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner
    {
        private EnemiesPool _enemiesPool;
        private EnemiesSpawnData _enemiesSpawnData;
        private bool _isSpawn = true;

        public void Init(EnemiesSpawnData enemiesSpawnData, EnemiesPool enemiesPool)
        {
            _enemiesSpawnData = enemiesSpawnData;
            _enemiesPool = enemiesPool;
        }

        public async UniTask StartSpawn()
        {
            while (_isSpawn)
            {
                if (_enemiesPool.TryGetAvailableEnemy(out EnemyController enemyController))
                {
                    var indexPoint = Random.Range(0, _enemiesSpawnData.SpawnPoints.Length);
                    enemyController.transform.position = _enemiesSpawnData.SpawnPoints[indexPoint].position;
                    enemyController.Health.Heal(enemyController.Health.MaxHealth);
                    enemyController.gameObject.SetActive(true);
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_enemiesSpawnData.SpawnCountdown));
            }
        }
    }
}