﻿using UnityEngine;

namespace RpgGame.Units
{
    public class UnitStatsComponent : MonoBehaviour
    {
        [Range(0.1f, 10f)]
        public float MoveSpeed = 3f;
        [Range(1f, 1000f)]
        public float MaxHealth = 20f;
        public Side Side;
        public string Name;
        
        public float CurrentHealth { get; set; } = 20f;
    }
}