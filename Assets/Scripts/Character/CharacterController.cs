using Interfaces;
using UnityEngine;
using UnityHFSM;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public abstract class CharacterController : MonoBehaviour
    {
        public IHealth Health => _health;
        
        protected AnimationHash _animationHash;
        protected Animator _animator;
        protected IHealth _health;
        protected IPhysicMovement _physicMovement;
        protected StateMachine<CharacterState> _fsm;
        protected float _attackDamage;


        protected void Init(IHealth health, IPhysicMovement physicMovement, float attackDamage)
        {
            _fsm = new StateMachine<CharacterState>();
            _animationHash = new AnimationHash();
            
            _animator = GetComponent<Animator>();

            _physicMovement = physicMovement;
            _health = health;
            _attackDamage = attackDamage;

        }

        protected abstract void InitFsmStates();
        protected abstract void InitFsmTransition();
        
        protected void Attack<T>(float hitRayHeight, float punchRange, LayerMask enemyLayerMask) where T: CharacterController
        {
            var rayStartPosition = new Vector3(transform.position.x,
                                               hitRayHeight,
                                               transform.position.z);

            Ray ray = new Ray(rayStartPosition, transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, punchRange, enemyLayerMask))
            {
                if (hit.collider.TryGetComponent(out T target))
                {
                    target.Health.TakeDamage((int)_attackDamage);
                }
            }
        }

        protected bool IsAnimationFinished(string name, int layerIndex, float threshold)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(layerIndex);
            return stateInfo.IsName(name) && stateInfo.normalizedTime >= threshold;
        }
        
        private void EndAttack()
        {
            _fsm.StateCanExit();
        }
    }
}