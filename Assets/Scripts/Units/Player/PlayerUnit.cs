using UnityEngine;

namespace RpgGame.Units.Player
{
    public class PlayerUnit : Unit
    {
        private CameraPoint _camera;

        // Вооружен ли unit
        private bool _armed;

        [SerializeField]
        private WeaponSet _weapon = WeaponSet.SwordAndShield;

        protected override void OnRotate()
        {
            transform.rotation = Quaternion
                .Euler(0f, _camera.PivotTransform.eulerAngles.y, 0f);
        }

        protected override void Start() {
            base.Start();
            _camera = FindObjectOfType<CameraPoint>();
        }
    }
}