using UnityEngine;

namespace RpgGame.Units.Player
{
    public class PlayerInputComponent : UnitInputComponent
    {
        private PlayerControls _controls;

        private void Awake() {
            _controls = new PlayerControls();
        }

        void Update()
        {
            var direction = _controls.Unit.Move.ReadValue<Vector2>();
            Movement = new Vector3(direction.x, 0f, direction.y);
            Debug.Log(Movement);
        }

        private void OnEnable() {
            _controls.Unit.Enable();
        }

        private void OnDisable() {
            _controls.Unit.Disable();
        }
        private void OnDestroy() {
            _controls.Dispose();
        }
    }
}