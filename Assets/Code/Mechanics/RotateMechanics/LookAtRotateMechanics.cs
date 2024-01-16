using System;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Code.Mechanics.RotateMechanics
{
    [Serializable]
    public class LookAtRotateMechanics : ObjectPart, IMechanics, IRotateMechanics, IInitializable
    {
        [SerializeField] private float _rotationSpeed = 25;
        
        private Transform _transform;


        public void Construct(DynamicObject dynamicObject)
        {
            _transform = dynamicObject.transform;
        }
        
        public void Rotate(Vector3 direction)
        {
            if(Enabled == false)
                return;

            Quaternion targetRotation = _transform.rotation;
            
            if(direction.magnitude > 0)
                 targetRotation = Quaternion.LookRotation(direction);
            
            _transform.rotation = 
                Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed); 
        }
    }
}