using System;
using System.Text;
using Interfaces;
using UI.View;

namespace UI.Presenter
{
    public class HealthPresenter : IDisposable
    {
        private HealthViewElements _healthViewElements;
        private IHealth _health;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void Init(IHealth health, HealthViewElements healthViewElements)
        {
            _health = health;
            _healthViewElements = healthViewElements;
            UpdateHealthViewElement();

            _health.OnHealthChanged += UpdateHealthViewElement;
        }

        private void UpdateHealthViewElement()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(_health.CurrentHealth);
            _stringBuilder.Append(" / ");
            _stringBuilder.Append(_health.MaxHealth);

            _healthViewElements.Health.text = _stringBuilder.ToString();
        }

        public void Dispose()
        {
            _health.OnHealthChanged -= UpdateHealthViewElement;
        }
    }
}