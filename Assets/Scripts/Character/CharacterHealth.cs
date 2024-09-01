using System;
using Interfaces;

namespace Character
{
    public class CharacterHealth : IHealth
    {
        public CharacterHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
            _isDead = false;
        }

        public event Action OnDeath;
        public event Action OnHealthChanged;

        public int CurrentHealth => _currentHealth;
        public bool IsDead => _isDead;
        public int MaxHealth => _maxHealth;

        private int _currentHealth;
        private bool _isDead;
        private int _maxHealth;

        public void Heal(int amount)
        {
            _isDead = false;
            
            _currentHealth += amount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            OnHealthChanged?.Invoke();
        }

        public void TakeDamage(int amount)
        {
            if (_isDead)
            {
                return;
            }

            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }
            else
            {
                OnHealthChanged?.Invoke();
            }
        }

        private void Die()
        {
            if (_isDead)
            {
                return;
            }

            _isDead = true;
            OnHealthChanged?.Invoke();
            OnDeath?.Invoke();
        }
    }
}