using Common.StateMachine;
using Cysharp.Threading.Tasks;
using Services.AssetManagement;
using Tutorial;
using UnityEngine;
using VContainer;

namespace GameStateMachines.States
{
    public class TutorialState : State
    {
        private SimpleAssetLoader _assetLoader;
        private IObjectResolver _objectResolver;
        private GameObject _tutorialGameObject;

        public TutorialState(IObjectResolver objectResolver,SimpleAssetLoader assetLoader)
        {
            _objectResolver = objectResolver;
            _assetLoader = assetLoader;
        }
        public override async UniTask Enter()
        {
           var tutorialPrefab = await _assetLoader.LoadAssetAsync<GameObject>(AssetKeys.TUTORIAL);
           _tutorialGameObject = Object.Instantiate(tutorialPrefab, null);
           _tutorialGameObject.GetComponent<TutorialController>().Init(_objectResolver.Resolve<GameStateMachine>());
        }
        
        public override UniTask Exit()
        {
            Object.Destroy(_tutorialGameObject);
            return UniTask.CompletedTask;
        }
    }
}
