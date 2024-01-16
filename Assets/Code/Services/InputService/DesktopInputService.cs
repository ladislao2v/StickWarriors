using Code.Mechanics.MovementMechanics;
using UnityEngine;

namespace Code.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        private readonly InputConfig _inputConfig;
        private readonly IMovementMechanics _movementMechanics;

        private Vector3 _direction = Vector3.zero;

        public DesktopInputService(InputConfig inputConfig, IMovementMechanics movementMechanics)
        {
            _inputConfig = inputConfig;
            _movementMechanics = movementMechanics;
        }

        public void Enable()
        {
            _inputConfig.Enable();
        }

        public void Disable()
        {
            _inputConfig.Disable();
        }
        public void OnUpdate( )
        {
            _direction.x = _inputConfig.Gameplay.Movement.ReadValue<Vector2>().x;
            _direction.z = _inputConfig.Gameplay.Movement.ReadValue<Vector2>().y;

            _movementMechanics.Move(_direction);
        }
    }
}