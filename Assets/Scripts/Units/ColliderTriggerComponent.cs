using UnityEngine;

namespace RpgGame.Units
{
    public class ColliderTriggerComponent : MonoBehaviour
    {
        private static Color _enabledColor = new Color(1f, 0f, 0f, 0.3f);
        private static Color _disabledColor = new Color(0f, 1f, 0f, 0.3f);

        private Collider _collider;

        [SerializeField]
        private long _id = 0;

        public long GetId => _id;

        public bool Enabled
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        void Start()
        {
            if (_collider == null)
                _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }

        private void OnValidate()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var unit = other.GetComponent<UnitStatsComponent>();
            if (unit != null)
            {
                unit.Health -= 5f;
                Debug.Log("Damage to: " + other.name);
                if (_id == 112)
                {
                    var body = other.GetComponent<Rigidbody>();
                    if (body != null)
                    {
                        body.constraints = RigidbodyConstraints.None;
                        other.transform.LookAt(_collider.transform);
                        body.AddForce(-other.transform.forward * 1000f, ForceMode.Impulse);
                    }
                }
                if (unit.Health <= 0f)
                    Destroy(unit.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Enabled ? _enabledColor : _disabledColor;
            Gizmos.DrawCube(_collider.bounds.center, _collider.bounds.size);
        }
    }
}