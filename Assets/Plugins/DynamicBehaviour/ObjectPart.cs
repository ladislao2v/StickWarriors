using System;
using UnityEngine;

namespace Plugins.DynamicBehaviour
{
    [Serializable]
    public abstract class ObjectPart
    {
        [field: SerializeField, Tooltip("Status")] 
        public bool Enabled { get; private set; } = true; 
        
        public void Enable()
        {
            Enabled = true;
            
            OnEnable();
        }

        public void Disable()
        {
            Enabled = false;
            
            OnDisable();
        }

        protected virtual void OnEnable() {}
        protected virtual void OnDisable() {}
    }
}