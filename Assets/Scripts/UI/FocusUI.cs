using RpgGame.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RpgGame.UI
{
    public class FocusUI : MonoBehaviour
    {
        [SerializeField]
        private Image _focusImage;
        [SerializeField]
        private Color _defaultColor;
        [SerializeField]
        private Color _friendColor;
        [SerializeField]
        private Color _enemyColor;
        [SerializeField]
        private TextMeshProUGUI _nameText;
        [SerializeField]
        private TextMeshProUGUI _healthText;
        [SerializeField, Space, Range(10f, 500f)]
        private float _maxDistance = 500f;

        private void LateUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(transform.position);
            if (Physics.Raycast(ray, out var hit, _maxDistance))
            {
                var unitStats = hit.transform.GetComponent<UnitStatsComponent>();
                if (unitStats)
                {
                    _nameText.text = unitStats.Name;
                    _healthText.text = $"{unitStats.CurrentHealth}/{unitStats.MaxHealth}";
                    Color focusColor;
                    switch (unitStats.Side)
                    {
                        case Side.Friend:
                            focusColor = _friendColor;
                            break;
                        case Side.Enemy:
                            focusColor = _enemyColor;
                            break;
                        default:
                            focusColor = _defaultColor;
                            break;
                    }
                    _focusImage.color = _nameText.color = focusColor;
                }
                else
                    ClearFocus();
            }
            else
                ClearFocus();
        }
        private void ClearFocus()
        {
            _nameText.text = _healthText.text = "";
            _focusImage.color = _nameText.color = _defaultColor;
        }
    }
}