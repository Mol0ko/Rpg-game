using UnityEngine;

namespace RpgGame.Units
{
    public class UnitStatsComponent : MonoBehaviour
    {
        [Range(0.1f, 10f)]
        public float MoveSpeed = 3f;
    }
}