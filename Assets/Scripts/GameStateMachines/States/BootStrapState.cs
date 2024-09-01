using Common.StateMachine;
using Cysharp.Threading.Tasks;
using Enemy;
using Factories;
using ScriptableObjects;
using Services.AssetManagement;
using Services.SceneLoader;

namespace GameStateMachines.States
{
    public class BootstrapState : State
    {
        private readonly AddressablesSceneLoader _addressableSceneLoader;
        private readonly PlayerFactory _playerFactory;
        private readonly BattleUIFactory _battleUIFactory;
        private readonly EnemiesFactory _enemiesFactory;
        private readonly SimpleAssetLoader _simpleAssetLoader;
        private readonly EnemiesPool _enemiesPool;
        private readonly EnemySpawner _enemySpawner;

        public BootstrapState(
            AddressablesSceneLoader addressableSceneLoader,
            SimpleAssetLoader simpleAssetLoader,
            PlayerFactory playerFactory,
            BattleUIFactory battleUIFactory,
            EnemiesFactory enemiesFactory,
            EnemiesPool enemiesPool,
            EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
            _enemiesPool = enemiesPool;
            _simpleAssetLoader = simpleAssetLoader;
            _enemiesFactory = enemiesFactory;
            _battleUIFactory = battleUIFactory;
            _playerFactory = playerFactory;
            _addressableSceneLoader = addressableSceneLoader;
        }

        public override async UniTask Enter()
        {
            await _addressableSceneLoader.LoadSceneAsync(AssetKeys.BATTLE_SCENE);
            var enemiesSpawnData =
                await _simpleAssetLoader.LoadAssetAsync<EnemiesSpawnData>(AssetKeys.ENEMIES_SPAWN_DATA);

            var playerController = await _playerFactory.Create();
            var canvas = await _battleUIFactory.Create();
            EnemyController[] enemyControllers = await _enemiesFactory.Create(enemiesSpawnData, playerController);
            
            _enemiesPool.Init(enemyControllers);
            _enemySpawner.Init(enemiesSpawnData, _enemiesPool);
            _stateMachine.Enter<TutorialState>().Forget();
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}