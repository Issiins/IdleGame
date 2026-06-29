using Player.Upgrade;
using UnityEngine;
using Upgrade;

namespace Interaction
{
    public class PointController : MonoBehaviour
    {
        private float _points = 0f;
        private UpgradeContainer _upgradeContainer;
        private readonly PlayerStats _stats = new();

        private void Awake()
        {
            _upgradeContainer = new UpgradeContainer(_stats);
        }
        public void OnClick()
        {
            _points += _stats.ClickMultiplier;
            Debug.Log("Current points after click" + _points);
        }
        private void Update()
        {
            _points += _stats.ProductionPerSecond * Time.deltaTime;
        }
        public float GetPoints() => _points;
    }
}