using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgGame.Units
{
    public class Unit : MonoBehaviour
    {
        private Animator _animator;
        private UnitInputComponent _input;
        private UnitStatsComponent _stats;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _input = GetComponent<UnitInputComponent>();
            _stats = GetComponent<UnitStatsComponent>();
        }

        // Update is called once per frame
        void Update()
        {
            ref var movement = ref _input.MoveDirection;
            _animator.SetFloat("ForwardMove", movement.z);
            _animator.SetFloat("SideMove", movement.x);
            var moving = movement.x != 0 || movement.z != 0;
            _animator.SetBool("Moving", moving);
            if (moving)
                transform.position += movement * _stats.MoveSpeed * Time.deltaTime;
        }
    }
}