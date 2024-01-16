using System;
using Code.Mechanics.HealthMechanics;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Code.Views.Animation
{
    [Serializable]
    public class DieAnimation : ObjectPart, IView, IInitializable
    {
        public static int Die = Animator.StringToHash(nameof(Die));
        
        [SerializeField] private Animator _animator;

        private IHealthMechanics _healthMechanics;
        
        public void Construct(DynamicObject dynamicObject)
        {
            if(dynamicObject.TryGetMechanics(out HealthMechanics healthMechanics))
                _healthMechanics ??= healthMechanics;
        }

        public void OnDied()
        {
            _animator.SetTrigger(Die);
        }

        protected override void OnEnable()
        {
            if (_healthMechanics == null)
                throw new InvalidOperationException(nameof(_healthMechanics));
            
            _healthMechanics.Died += OnDied;
        }

        protected override void OnDisable()
        {
            _healthMechanics.Died -= OnDied;
        }
    }
}