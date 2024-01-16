using System;
using Code.Mechanics.MovementMechanics;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Code.Views.Animation
{
    [Serializable]
    public class MovementAnimation : ObjectPart, IView, IInitializable
    {
        public static int Magnitude = Animator.StringToHash(nameof(Magnitude));
        
        [SerializeField] private Animator _animator;
        
        private IMovementMechanics _movementMechanics;

        public void Construct(DynamicObject dynamicObject)
        {
            if (dynamicObject.TryGetMechanics(out IMovementMechanics movementMechanics))
                _movementMechanics ??= movementMechanics;
        }

        public void OnMoved(float speed)
        {
            if (_movementMechanics == null)
                throw new InvalidOperationException(nameof(_movementMechanics));
            
            if (CanPlay())
                return;

            _animator.SetFloat(Magnitude, speed);
        }

        protected override void OnEnable()
        {
            _movementMechanics.OnMoved += OnMoved;
        }

        protected override void OnDisable()
        {
            _movementMechanics.OnMoved -= OnMoved;
        }

        private bool CanPlay()
        {
            return Enabled == false || ((IMechanics) _movementMechanics).Enabled == false;
        }
    }
}