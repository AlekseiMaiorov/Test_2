using Cysharp.Threading.Tasks;
using Interfaces;
using Player;
using ScriptableObjects;
using Services.AssetManagement;
using UI.Presenter;
using UI.View;
using VContainer;
using VContainer.Unity;

namespace Factories
{
    public class PlayerFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly SimpleAssetLoader _simpleAssetLoader;
        private readonly IInput _input;

        public PlayerFactory(
            IObjectResolver objectResolver,
            SimpleAssetLoader simpleAssetLoader,
            IInput input)
        {
            _input = input;
            _objectResolver = objectResolver;
            _simpleAssetLoader = simpleAssetLoader;
        }

        public async UniTask<PlayerController> Create()
        {
            var playerData = await _simpleAssetLoader.LoadAssetAsync<CharacterData>(AssetKeys.PLAYER_DATA);
            var playerGameObject = _objectResolver.Instantiate(playerData.Prefab, null);

            var playerController = playerGameObject.GetComponent<PlayerController>();
            var healthView = playerGameObject.GetComponentInChildren<HealthViewElements>();
            var healthPresenter = _objectResolver.Resolve<HealthPresenter>();

            playerController.Init(_input, playerData.MoveSpeed, playerData.AttackDamage, playerData.MaximumHealth);
            healthPresenter.Init(playerController.Health, healthView);

            return playerController;
        }
    }
}