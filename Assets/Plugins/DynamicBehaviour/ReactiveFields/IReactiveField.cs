using System;

namespace Plugins.DynamicBehaviour.ReactiveFields
{
    public interface IReactiveField<TFieldType>
    {
        public TFieldType Value { get; set; }

        public void Subscribe(Action<TFieldType> action);
        public void Unsubscribe(Action<TFieldType> action);
    }
}