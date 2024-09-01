using UnityEngine;

namespace Interfaces
{
    public interface IInput
    {
        Vector3 GetMovementInput();
        bool IsAttackPressed();
    }
}