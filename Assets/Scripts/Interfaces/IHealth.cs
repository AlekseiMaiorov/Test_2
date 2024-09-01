using System;

namespace Interfaces
{
    public interface IHealth
    {
        event Action OnHealthChanged;
        event Action OnDeath;

        int MaxHealth { get; }
        int CurrentHealth { get; }
        bool IsDead { get; }
        
        void Heal(int amount);
        void TakeDamage(int amount);
    }
}