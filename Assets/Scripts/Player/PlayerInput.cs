using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerInput: IInput
    {
        public Vector3 GetMovementInput()
        {
            float h = SimpleInput.GetAxis("Horizontal");
            float v = SimpleInput.GetAxis("Vertical");
            return new Vector3(h, 0, v);
        }

        public bool IsAttackPressed()
        {
            return SimpleInput.GetButtonDown("Attack");
        }
    }
}