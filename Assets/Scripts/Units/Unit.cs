using System;
using System.Linq;
using UnityEngine;

namespace RpgGame.Units
{
    public class Unit : MonoBehaviour
    {
        private Animator _animator;
        private UnitInputComponent _input;
        private UnitStatsComponent _stats;
        private bool _playingAttackAnimation = false;

        [SerializeField]
        private Transform _targetPoint;
        [SerializeField]
        private ColliderTriggerComponent[] _colliderTriggers;

        public Unit Target { get; protected set; }
        public Transform GetTargetPoint => _targetPoint;
        public Action OnTargetLostHandler;

        #region Lifecycle

        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _input = GetComponent<UnitInputComponent>();
            _stats = GetComponent<UnitStatsComponent>();
            _colliderTriggers = GetComponentsInChildren<ColliderTriggerComponent>();

            if (_input != null)
            {
                _input.OnAttackHandler += OnAttack;
                _input.OnTargetEvent += OnTargetUpdate;
            }
        }

        protected virtual void Update()
        {
            if (!_playingAttackAnimation)
                OnMove();
        }

        #endregion

        private void OnMove()
        {
            ref var movement = ref _input.MoveDirection;
            _animator.SetFloat("ForwardMove", movement.z);
            _animator.SetFloat("SideMove", movement.x);
            var moving = movement.x != 0 || movement.z != 0;
            _animator.SetBool("Moving", moving);
            if (moving)
                transform.position += movement * _stats.MoveSpeed * Time.deltaTime;
        }

        private void OnAttack(Weapon weapon)
        {
            if (!_playingAttackAnimation)
            {
                var animationName = weapon.ToString() + "Attack";
                _animator.SetTrigger(animationName);
                _playingAttackAnimation = true;
            }
        }

        private void OnTargetUpdate()
        {
            if (Target != null)
            {
                Target = null;
                // TODO: temp solution
                OnTargetLostHandler?.Invoke();
            }
            else
            {
                var distanceToTarget = float.MaxValue;
                UnitStatsComponent target = null;
                // TODO: fix FindObjectsOfType
                var units = FindObjectsOfType<UnitStatsComponent>();
                foreach (var unit in units)
                {
                    if (unit.Side != _stats.Side)
                    {
                        var currentDistance = (unit.transform.position - transform.position).sqrMagnitude;
                        if (currentDistance < distanceToTarget)
                        {
                            distanceToTarget = currentDistance;
                            target = unit;
                        }
                    }
                }

                if (target != null)
                    Target = target.GetComponent<Unit>();
                else // TODO: handle null target
                    Debug.LogError("[Unit] No units found to set target");

                Debug.Log("[Unit] target: " + Target?.GetHashCode());
            }
        }

        public void OnAttackAnimationEnd(AnimationEvent data)
        {
            _playingAttackAnimation = false;
        }

        public void OnColliderControlEvent(AnimationEvent data)
        {
            var trigger = _colliderTriggers
                .FirstOrDefault(c => c.GetId == data.intParameter);
#if UNITY_EDITOR
            if (trigger == null)
            {
                Debug.LogError($"Коллайдер не найден: {data.intParameter}");
                UnityEditor.EditorApplication.isPaused = true;
            }
#endif
            trigger.Enabled = data.floatParameter == 1;
        }
    }
}