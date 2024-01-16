using System;
using Code.Mechanics.MovementMechanics;
using Code.Services.InputService;
using Plugins.DynamicBehaviour;
using UnityEngine;

namespace Code
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private DynamicObject _player;

        private IInputService _inputService;
        
        private void Awake()
        {
            if (_player.TryGetMechanics(out IMovementMechanics movementMechanics))
                _inputService = new DesktopInputService(new InputConfig(), movementMechanics);
        }

        private void OnEnable()
        {
            _inputService.Enable();
        }

        private void OnDisable()
        {
            _inputService.Enable();
        }

        private void Update()
        {
            _inputService.OnUpdate();
        }
    }
}