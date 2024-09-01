using Cysharp.Threading.Tasks;
using Enemy;
using Player;
using ScriptableObjects;
using Services.AssetManagement;
using UI.Presenter;
using UI.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Factories
{
    public class EnemiesFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly SimpleAssetLoader _simpleAssetLoader;

        public EnemiesFactory(
            IObjectResolver objectResolver,
            SimpleAssetLoader simpleAssetLoader)
        {
            _objectResolver = objectResolver;
            _simpleAssetLoader = simpleAssetLoader;
        }

        public async UniTask<EnemyController[]> Create(EnemiesSpawnData spawnData, PlayerController playerController)
        {
            var enemyData = await _simpleAssetLoader.LoadAssetAsync<CharacterData>(AssetKeys.ENEMY_DATA);
            var parent = new GameObject("---Enemies---");
            var enemyControllers = new EnemyController[spawnData.MaxEnemies];

            for (var index = 0; index < spawnData.MaxEnemies; index++)
            {
                var enemyGameObject = _objectResolver.Instantiate(enemyData.Prefab, parent.transform);
                var enemyController = enemyGameObject.GetComponent<EnemyController>();

                var healthView = enemyGameObject.GetComponentInChildren<HealthViewElements>();

                var healthPresenter = _objectResolver.Resolve<HealthPresenter>();

                enemyController.Init(enemyData.MoveSpeed,
                                     enemyData.AttackDamage,
                                     playerController.gameObject,
                                     enemyData.MaximumHealth);
                healthPresenter.Init(enemyController.Health, healthView);

                enemyController.gameObject.SetActive(false);
                enemyControllers[index] = enemyController;
            }

            return enemyControllers;
        }
    }
}