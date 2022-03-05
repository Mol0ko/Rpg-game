using UnityEngine;

namespace RpgGame.Units.Player
{
    public class CameraPoint : MonoBehaviour
    {
        private PlayerControls _controls;
        private Unit _target;

        private Transform _pivot;
        private Transform _camera;

        private Vector2 _angle = Vector2.zero;
        // Поворот пивота по вертикали
        private Quaternion _pivotTargetRotation;
        // Поворот точки по горизонтали
        private Quaternion _transformTargetRotation;
        private Vector3 _initialPivotEulerAngles;

        [SerializeField, Range(-90f, 0f), Tooltip("Мин наклон камеры по вертикали")]
        private float _minY = -45f;
        [SerializeField, Range(0f, 90f), Tooltip("Макс наклон камеры по вертикали")]
        private float _maxY = 30f;
        [SerializeField, Range(0.5f, 10f)]
        private float _moveSpeed = 6f;
        [SerializeField, Range(0.1f, 5f)]
        private float _rotateSpeed = 0.5f;

        [SerializeField, Range(10, 0.1f), Tooltip("Сглаживание вращения камеры")]
        private float _smoothing = 10;

        #region Lifecycle

        private void Awake()
        {
            _controls = new PlayerControls();
        }

        private void Start()
        {
            _target = transform.parent.GetComponent<Unit>();
            _pivot = transform.GetChild(0);
            _initialPivotEulerAngles = _pivot.eulerAngles;
            _camera = GetComponentInChildren<Camera>().transform;
            transform.parent = null;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.DrawSphere(_target.transform.position, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.15f);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_pivot.position, _pivot.forward);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_camera.position, _camera.forward);
        }
        #endif

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _target.transform.position,
                Time.deltaTime * _moveSpeed);
            FreeCamera();
        }

        private void OnEnable()
        {
            _controls.Camera.Enable();
        }

        private void OnDisable()
        {
            _controls.Camera.Disable();
        }
        private void OnDestroy()
        {
            _controls.Dispose();
        }

        #endregion

        private void FreeCamera()
        {
            var delta = _controls.Camera.Delta.ReadValue<Vector2>();
            Debug.Log("Mouse delta: " + delta);
            _angle += delta * _rotateSpeed;
            _angle.y = Mathf.Clamp(_angle.y, _minY, _maxY);

            _pivotTargetRotation = Quaternion.Euler(
                _angle.y,
                _initialPivotEulerAngles.y,
                _initialPivotEulerAngles.z);
            _transformTargetRotation = Quaternion.Euler(0f, _angle.x, 0f);

            _pivot.localRotation = Quaternion.Slerp(
                _pivot.localRotation, 
                _pivotTargetRotation, 
                _smoothing * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation, 
                _transformTargetRotation, 
                _smoothing * Time.deltaTime);
        }
    }
}