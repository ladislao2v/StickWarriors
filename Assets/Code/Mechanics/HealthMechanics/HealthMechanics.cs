using System;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Mechanics.HealthMechanics
{
    [Serializable]
    public class HealthMechanics : ObjectPart, IMechanics, IHealthMechanics, IInitializable
    {
        [SerializeField] private int _maxHealth;
        
        private int _currentHealth;

        public event Action Died;
        public event Action<int> HealthChanged;

        public void Construct(DynamicObject dynamicObject)
        {
            _currentHealth = _maxHealth;
        }

        [Button]
        public void TakeDamage(int damage)
        {
            if(Enabled == false)
                return;
            
            if (damage < 0)
                throw new ArgumentException(nameof(damage));

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            
            Debug.Log(_currentHealth);
            HealthChanged?.Invoke(_currentHealth);
            
            if(_currentHealth <= 0)
                Died?.Invoke();
        }

        [Button]
        public void Hill(int hillPoints)
        {
            if(Enabled == false)
                return;
            
            if(hillPoints < 0)
                throw new ArgumentException(nameof(hillPoints));
            
            HealthChanged?.Invoke(_currentHealth);

            _currentHealth = Mathf.Clamp(_currentHealth + hillPoints, 0, _maxHealth);
        }
    }
}