using System.Collections;
using Player.Upgrade;
using UnityEngine;
using Upgrade;

namespace Interaction
{
    public class PointController : MonoBehaviour
    {
        [SerializeField] private float points = 0f;
        private UpgradeContainer _upgradeContainer;
        private readonly PlayerStats _stats = new();
        private readonly WaitForSeconds _waitTime = new(1f); 
        private void Awake()
        {
            UpgradeFactory upgradeFactory = new();
            _upgradeContainer = new UpgradeContainer(_stats, upgradeFactory.GetUpgrades());
            StartCoroutine(nameof(Accumalate));
        }
        public void OnClick()
        {
            points += _stats.ClickMultiplier;
        }
        public float GetPoints() => points;

        private IEnumerator Accumalate()
        {
            while (true)
            {
                points += _stats.ProductionPerSecond;
                yield return _waitTime;   
            }
        }
        
    }
}