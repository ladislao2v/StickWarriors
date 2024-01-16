using System;
using Code.Mechanics.HealthMechanics;
using Code.Mechanics.MoveMechanics;
using Code.Mechanics.RotateMechanics;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Code.Mechanics.MovementMechanics
{
    [Serializable]
    public class PlayerMovementMechanics : ObjectPart, IMechanics, IMovementMechanics, IInitializable, IDisposable
    {
        [SerializeReference] private IMoveMechanics _moveMechanics;
        [SerializeReference] private IRotateMechanics _rotateMechanics;

        private IHealthMechanics _healthMechanics;

        public event Action<float> OnMoved;

        public void Construct(DynamicObject dynamicObject)
        {
            if (dynamicObject.TryGetMechanics(out IHealthMechanics healthMechanics))
                _healthMechanics ??= healthMechanics;
            
            ((IInitializable)_rotateMechanics).Construct(dynamicObject);

            _healthMechanics.Died += Disable;
        }

        public void Move(Vector3 direction)
        {
            if(Enabled == false)
                return;

            OnMoved?.Invoke(direction.magnitude);
            
            _moveMechanics.Move(direction);
            _rotateMechanics.Rotate(direction);
        }

        public void Dispose()
        {
            _healthMechanics.Died -= Disable;
        }
    }
}