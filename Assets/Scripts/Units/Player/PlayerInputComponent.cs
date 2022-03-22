using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RpgGame.Units.Player
{
    public class PlayerInputComponent : UnitInputComponent
    {
        private PlayerControls _controls;

        public Action MeleeSetEventHandler;
        public Action RangeSetEventHandler;

        #region Lifecycle

        private void Awake() {
            _controls = new PlayerControls();
            _controls.Unit.MainWeaponAttack.performed += OnMainWeaponAttack;
            _controls.Unit.AdditionalWeaponAttack.performed += OnAdditionalWeaponAttack;
            _controls.Unit.LockTarget.performed += OnTargetLock;
            _controls.Unit.MeleeSet.performed += OnMeleeSet;
            _controls.Unit.RangeSet.performed += OnRangeSet;
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
            _controls.Unit.MainWeaponAttack.performed -= OnMainWeaponAttack;
            _controls.Unit.AdditionalWeaponAttack.performed -= OnAdditionalWeaponAttack;
            _controls.Unit.LockTarget.performed -= OnTargetLock;
            _controls.Dispose();
        }

        #endregion

        private void OnMainWeaponAttack(InputAction.CallbackContext obj) =>
            CallOnAttackEvent(Weapon.MainWeapon);

        private void OnAdditionalWeaponAttack(InputAction.CallbackContext obj) =>
            CallOnAttackEvent(Weapon.AdditionalWeapon);

        private void OnTargetLock(InputAction.CallbackContext obj) => 
            CallOnTargetEvent();

        private void OnMeleeSet(InputAction.CallbackContext obj) => 
            MeleeSetEventHandler?.Invoke();

        private void OnRangeSet(InputAction.CallbackContext obj) =>
            RangeSetEventHandler?.Invoke();
    }
}