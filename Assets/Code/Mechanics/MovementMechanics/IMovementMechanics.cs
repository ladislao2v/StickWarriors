
using System;
using UnityEngine;

namespace Code.Mechanics.MovementMechanics
{
    public interface IMovementMechanics
    {
        event Action<float> OnMoved; 
        void Move(Vector3 direction);
    }
}