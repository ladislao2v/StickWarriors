using System;
using Code.Mechanics.HealthMechanics;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Code.Views.Animation
{
    [Serializable]
    public class HitAnimation : ObjectPart, IView, IInitializable
    {
        public static int Hit = Animator.StringToHash(nameof(Hit));
        
        [SerializeField] private Animator _animator;

        private IHealthMechanics _healthMechanics;
        
        public void Construct(DynamicObject dynamicObject)
        {
            if(dynamicObject.TryGetMechanics(out HealthMechanics healthMechanics))
                _healthMechanics ??= healthMechanics;
        }

        private void OnHealthChanged(int value)
        {
            _animator.SetTrigger(Hit);
        }

        protected override void OnEnable()
        {
            if (_healthMechanics == null)
                throw new InvalidOperationException(nameof(_healthMechanics));
            
            _healthMechanics.HealthChanged += OnHealthChanged;
        }

        protected override void OnDisable()
        {
            _healthMechanics.HealthChanged -= OnHealthChanged;
        }
    }
}