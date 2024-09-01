using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class AddressablesSceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneKey, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneKey, loadSceneMode);

            try
            {
                await handle.ToUniTask();

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"Сцена {sceneKey} загружена успешно.");
                }
                else
                {
                    Debug.LogError($"Не удалось загрузить сцену с ключом: {sceneKey}");
                }
            }
            catch
            {
                Debug.LogError($"Произошла ошибка при загрузке сцены с ключом: {sceneKey}");
            }
        }
    }
}