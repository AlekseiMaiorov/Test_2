using TMPro;
using UnityEngine;

namespace UI.View
{
    public class HealthViewElements : MonoBehaviour
    {
        public TextMeshProUGUI Health => _health;

        [SerializeField]
        private TextMeshProUGUI _health;
    }
}