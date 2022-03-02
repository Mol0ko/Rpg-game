using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame.Units
{
    public class UnitInputComponent : MonoBehaviour
    {
        protected  Vector3 Movement;

        public ref Vector3 MoveDirection => ref Movement;
    }
}