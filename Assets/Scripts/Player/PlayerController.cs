using Character;
using Enemy;
using Interfaces;
using UnityEngine;
using UnityHFSM;
using CharacterController = Character.CharacterController;

namespace Player
{
    public class PlayerController : CharacterController
    {
        private LayerMask _enemyLayerMask;
        private IInput _input;
        private float _punchRange;
        private float _hitRayHeight;

        public void Init(IInput input, float moveSpeed, float attackDamage, int maxHealth)
        {
            _enemyLayerMask = LayerMask.GetMask("Enemy");
            _punchRange = 1;
            _hitRayHeight = 1;
            _input = input;

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
                          onEnter: state => _animator.SetBool(_animationHash.Walk, false),
                          onLogic: state => _physicMovement.SetAxisValue(Vector3.zero));

            _fsm.AddState(CharacterState.Walk,
                          onEnter: state => _animator.SetBool(_animationHash.Walk, true),
                          onLogic: state => _physicMovement.SetAxisValue(_input.GetMovementInput()));

            _fsm.AddState(CharacterState.Attack,
                          onEnter: state =>
                                   {
                                       _animator.SetTrigger(_animationHash.Attack);
                                       _physicMovement.SetAxisValue(Vector3.zero);
                                       Attack<EnemyController>(_hitRayHeight, _punchRange, _enemyLayerMask);
                                   },
                          onLogic: state => _physicMovement.SetAxisValue(_input.GetMovementInput()),
                          needsExitTime: true);

            _fsm.AddState(CharacterState.Dead,
                          onEnter: state =>
                                   {
                                       _animator.SetBool(_animationHash.Dead, true);
                                       _physicMovement.SetAxisValue(Vector3.zero);
                                   });
        }

        protected override void InitFsmTransition()
        {
            _fsm.AddTwoWayTransition(CharacterState.Idle,
                                     CharacterState.Walk,
                                     transition => _input.GetMovementInput().magnitude > 0);

            _fsm.AddTransition(CharacterState.Attack, CharacterState.Idle);

            _fsm.AddTransitionFromAny(CharacterState.Attack,
                                      transition => _input.IsAttackPressed() && !Health.IsDead);

            _fsm.AddTransitionFromAny(CharacterState.Dead, transition => Health.IsDead);
            _fsm.AddTransition(CharacterState.Dead, CharacterState.Idle, transition => !Health.IsDead);
        }
    }
}