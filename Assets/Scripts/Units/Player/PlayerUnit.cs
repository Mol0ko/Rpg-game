using UnityEngine;

namespace RpgGame.Units.Player
{
    public class PlayerUnit : Unit
    {
        private CameraPoint _camera;

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