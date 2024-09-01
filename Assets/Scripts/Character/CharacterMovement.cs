using Interfaces;
using UnityEngine;

namespace Character
{
    public class CharacterMovement : IPhysicMovement
    {
        public CharacterMovement(float moveSpeed, Rigidbody rigidbody)
        {
            _moveSpeed = moveSpeed;
            _rigidbody = rigidbody;

            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = false;
        }

        private readonly float _moveSpeed;
        private readonly Rigidbody _rigidbody;
        private readonly float _rotationSpeed = 5f;
        private float _verticalAxis;
        private float _horizontalAxis;

        public void SetAxisValue(Vector3 axis)
        {
            _horizontalAxis = axis.x;
            _verticalAxis = axis.z;
        }

        public void Move(Transform transform)
        {
            Vector3 moveDirection = new Vector3(_horizontalAxis, 0, _verticalAxis).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                _rigidbody.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed);
            }
            else
            {
                _rigidbody.angularVelocity = Vector3.zero;
            }

            Vector3 movement = moveDirection * _moveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + movement);
        }
    }
}