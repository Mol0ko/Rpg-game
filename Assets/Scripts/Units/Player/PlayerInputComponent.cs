using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RpgGame.Units.Player
{
    public class PlayerInputComponent : UnitInputComponent
    {
        private PlayerControls _controls;

        #region Lifecycle

        private void Awake() {
            _controls = new PlayerControls();
            _controls.Unit.SwordAttack.performed += OnSwordAttack;
        }

        void Update()
        {
            var direction = _controls.Unit.Move.ReadValue<Vector2>();
            Movement = new Vector3(direction.x, 0f, direction.y);
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

        #endregion

        private void OnSwordAttack(InputAction.CallbackContext obj)
        {
            CallOnAttackEvent();
        }
    }
}