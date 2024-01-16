using System;
using Plugins.DynamicBehaviour;
using Plugins.DynamicBehaviour.Interfaces;
using UnityEngine;

namespace Code.Mechanics.CameraFollowerMechanics
{
    [Serializable]
    public class CameraFollowerMechanics : ObjectPart, IMechanics, ICameraFollowerMechanics, ILateUpdatable, IInitializable
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _yOffset;
        [SerializeField] private float _delaySpeed;

        private Transform _camera;
        
        public void LateUpdate()
        {
            Follow();
        }

        public void Construct(DynamicObject dynamicObject)
        {
            _camera = dynamicObject.transform;
        }

        public void Follow()
        {
            if(Enabled == false)
                return;
            
            float x = Mathf.Lerp(_camera.position.x, _target.position.x, Time.deltaTime * _delaySpeed);
            float z = Mathf.Lerp(_camera.position.z, _target.position.z - _yOffset, Time.deltaTime * _delaySpeed);
            
            _camera.position = new Vector3(x, _camera.position.y, z);
        }
    }
}