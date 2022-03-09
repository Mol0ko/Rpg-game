using System;
using UnityEngine;

namespace RpgGame.Units
{
    public class UnitInputComponent : MonoBehaviour
    {
        protected  Vector3 Movement;
        public Action OnAttackHandler;
        public Action OnTargetEvent;

        public ref Vector3 MoveDirection => ref Movement;

        public void CallOnAttackEvent() => OnAttackHandler?.Invoke();
        public void CallOnTargetEvent() => OnTargetEvent?.Invoke();
    }
}