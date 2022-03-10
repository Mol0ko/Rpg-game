using System;
using UnityEngine;

namespace RpgGame.Units
{
    public class UnitInputComponent : MonoBehaviour
    {
        protected  Vector3 Movement;
        public Action<Weapon> OnAttackHandler;
        public Action OnTargetEvent;

        public ref Vector3 MoveDirection => ref Movement;

        public void CallOnAttackEvent(Weapon weapon) => OnAttackHandler?.Invoke(weapon);
        public void CallOnTargetEvent() => OnTargetEvent?.Invoke();
    }
}