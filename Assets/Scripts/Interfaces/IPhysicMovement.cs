using UnityEngine;

namespace Interfaces
{
    public interface IPhysicMovement
    {
        void SetAxisValue(Vector3 axis);
        void Move(Transform transform);
    }
}