using UnityEngine;

namespace Character
{
    public class AnimationHash
    {
        public int Walk { get; } = Animator.StringToHash("IsWalking");
        public int Dead { get; } = Animator.StringToHash("IsDeath");
        public int Attack { get; } = Animator.StringToHash("Attack");
    }
}