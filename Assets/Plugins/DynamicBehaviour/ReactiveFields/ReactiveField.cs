using System;
using UnityEngine;

namespace Plugins.DynamicBehaviour.ReactiveFields
{
    [Serializable]
    public class ReactiveField<TFieldType> : IReactiveField<TFieldType>
    {
        [SerializeField] private TFieldType _value;

        public TFieldType Value 
        { 
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value); 
            }
        }

        private event Action<TFieldType> OnValueChanged;

        public void Subscribe(Action<TFieldType> action)
        {
            OnValueChanged += action;
        }

        public void Unsubscribe(Action<TFieldType> action)
        {
            OnValueChanged -= action;
        }
    }
}