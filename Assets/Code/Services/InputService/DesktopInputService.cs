using Code.Mechanics.MovementMechanics;
using Code.Views.Animation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        private readonly InputConfig _inputConfig;
        private readonly IMovementMechanics _movementMechanics;
        private readonly AttackAnimation _attackAnimation;

        private Vector3 _direction = Vector3.zero;

        public bool IsStopped => _direction.magnitude == 0;

        public DesktopInputService(InputConfig inputConfig, IMovementMechanics movementMechanics, AttackAnimation attackAnimation)
        {
            _inputConfig = inputConfig;
            _movementMechanics = movementMechanics;
            _attackAnimation = attackAnimation;
        }

        public void Enable()
        {
            _inputConfig.Enable();
            _inputConfig.Gameplay.Attack.performed += OnLeftButtonPressed;
        }

        public void Disable()
        {
            _inputConfig.Disable();
            _inputConfig.Gameplay.Attack.performed -= OnLeftButtonPressed;
        }

        private void OnLeftButtonPressed(InputAction.CallbackContext obj)
        {
            if(IsStopped == false)
                return;
            
            _attackAnimation.OnAttack();
        }

        public void OnUpdate( )
        {
            _direction.x = _inputConfig.Gameplay.Movement.ReadValue<Vector2>().x;
            _direction.z = _inputConfig.Gameplay.Movement.ReadValue<Vector2>().y;

            _movementMechanics.Move(_direction);
        }
    }
}