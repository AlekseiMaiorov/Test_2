using UnityEngine;

namespace Common.UI
{
    public class LookAtUI : MonoBehaviour
    {
        private Camera _mainCamera;

        void Start()
        {
            _mainCamera = Camera.main;
        }

        void Update()
        {
            if (_mainCamera != null)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            }
        }
    }
}