using UnityEngine;

namespace RpgGame.Units
{
    public class Unit : MonoBehaviour
    {
        private Animator _animator;
        private UnitInputComponent _input;
        private UnitStatsComponent _stats;

        private bool _playingAttackAnimation = false;

        #region Lifecycle

        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _input = GetComponent<UnitInputComponent>();
            _stats = GetComponent<UnitStatsComponent>();

            if (_input != null)
                _input.OnAttackHandler += OnSwordAttack;
        }

        void Update()
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

        private void OnSwordAttack()
        {
            if (!_playingAttackAnimation) {
                _animator.SetTrigger("SwordAttack");
                _playingAttackAnimation = true;
            }
        }

        public void OnSwordAttackAnimationEnd(AnimationEvent Data)
        {
            _playingAttackAnimation = false;
        }
    }
}