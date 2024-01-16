using System;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Code.Mechanics.MoveMechanics
{
    [Serializable]
    public class SimpleMoveMechanics : ObjectPart, IMechanics, IMoveMechanics
    {
        [SerializeField] private float _speed;
        [SerializeField] private CharacterController _characterController;

        public void Move(Vector3 direction)
        {
            if (Enabled == false)
                return;

            _characterController.Move(CalculateOffset(direction));
        }

        private Vector3 CalculateOffset(Vector3 direction)
        {
            return direction * (_speed * Time.deltaTime);
        }
    }
}
