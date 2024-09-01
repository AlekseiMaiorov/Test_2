using Cysharp.Threading.Tasks;
using Services.AssetManagement;
using UnityEngine;
using VContainer;

namespace Factories
{
    public class BattleUIFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly SimpleAssetLoader _simpleAssetLoader;

        public BattleUIFactory(
            IObjectResolver objectResolver,
            SimpleAssetLoader simpleAssetLoader)
        {
            _objectResolver = objectResolver;
            _simpleAssetLoader = simpleAssetLoader;
        }

        public async UniTask<GameObject> Create()
        {
            var canvasPrefab = await _simpleAssetLoader.LoadAssetAsync<GameObject>(AssetKeys.CANVAS_BATTLE);
            var canvas = Object.Instantiate(canvasPrefab, null, true);
            return canvas;
        }
    }
}