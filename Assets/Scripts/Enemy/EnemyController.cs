using Character;
using Player;
using UnityEngine;
using UnityHFSM;

namespace Enemy
{
    public class EnemyController : Character.CharacterController
    {
        private LayerMask _targetLayerMask;
        private float _punchRange;
        private float _hitRayHeight;
        private float _attackDistance;
        private GameObject _target;

        public void Init(float moveSpeed, float attackDamage, GameObject target, int maxHealth)
        {
            _target = target;
            _targetLayerMask = 1 << target.layer;
            _punchRange = 1;
            _hitRayHeight = 1;
            _attackDistance = 0.9f;

            var movement = new CharacterMovement(moveSpeed, GetComponent<Rigidbody>());
            var characterHealth = new CharacterHealth(maxHealth);
            base.Init(characterHealth, movement, attackDamage);

            InitFsmStates();
            InitFsmTransition();

            _fsm.SetStartState(CharacterState.Idle);
            _fsm.Init();
        }

        private void FixedUpdate()
        {
            _physicMovement.Move(transform);
        }

        private void Update()
        {
            _fsm.OnLogic();
        }

        protected override void InitFsmStates()
        {
            _fsm.AddState(CharacterState.Idle,
                          onEnter: state =>
                                   {
                                       _animator.SetBool(_animationHash.Walk, false);
                                       _physicMovement.SetAxisValue(Vector3.zero);

                                       var sqrDistance = TargetDirection().sqrMagnitude;
                                       if ((_attackDistance * _attackDistance) >= sqrDistance)
                                       {
                                           _fsm.RequestStateChange(CharacterState.Attack);
                                       }
                                   });

            _fsm.AddState(CharacterState.Walk,
                          onEnter: state => _animator.SetBool(_animationHash.Walk, true),
                          onLogic: state => _physicMovement.SetAxisValue(TargetDirection().normalized));

            _fsm.AddState(CharacterState.Attack,
                          onEnter: state =>
                                   {
                                       _animator.SetTrigger(_animationHash.Attack);
                                       _physicMovement.SetAxisValue(Vector3.zero);
                                       Attack<PlayerController>(_hitRayHeight, _punchRange, _targetLayerMask);
                                   },
                          canExit: state =>
                                   {
                                       var sqrDistance = TargetDirection()
                                          .sqrMagnitude;

                                       return (_attackDistance * _attackDistance) < sqrDistance;
                                   },
                          needsExitTime: true);

            _fsm.AddState(CharacterState.Dead,
                          onEnter: state =>
                                   {
                                       _animator.SetBool(_animationHash.Dead, true);
                                       _physicMovement.SetAxisValue(Vector3.zero);
                                   },
                          onLogic: state =>
                                   {
                                       var animationDuration = 1f;
                                       if (IsAnimationFinished("Dead", 0, animationDuration))
                                       {
                                           gameObject.SetActive(false);
                                       }
                                   });
        }

        protected override void InitFsmTransition()
        {
            _fsm.AddTwoWayTransition(CharacterState.Idle,
                                     CharacterState.Walk,
                                     transition =>
                                     {
                                         var sqrDistance = TargetDirection().sqrMagnitude;

                                         return (_attackDistance * _attackDistance) < sqrDistance;
                                     });

            _fsm.AddTransition(CharacterState.Attack, CharacterState.Idle);

            _fsm.AddTransitionFromAny(CharacterState.Dead, transition => Health.IsDead);
            _fsm.AddTransition(CharacterState.Dead, CharacterState.Idle, transition => !Health.IsDead);
        }

        private Vector3 TargetDirection()
        {
            return _target.transform.position - transform.position;
        }
    }
}