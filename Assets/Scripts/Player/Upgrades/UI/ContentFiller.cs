using System.Collections.Generic;
using Interaction;
using UnityEngine;
using Upgrades;

namespace Player.Upgrade.UI
{
    public class ContentFiller : MonoBehaviour
    {
        [SerializeField] private PointController controller;
        [SerializeField] private GameObject prefab;
        private IReadOnlyList<UpgradeInstance> _upgrades;
        private Transform _contentsTransform;
        private UpgradeContainer _upgradeContainer;
        private List<UpgradeContent> _contents = new();
        private void Start()
        {
            _upgrades = controller.GetUpgrades();
            _upgradeContainer = controller.GetUpgradeContainer();
            _upgradeContainer.OnUpgradePurchased += RefreshUI;
            _contentsTransform = gameObject.transform;
            FillContent();
        }
        /// <summary>
        /// Refreshes whole shop UI
        /// </summary>
        private void RefreshUI()
        {
            foreach (UpgradeContent content in _contents)
                content.Refresh();
        }

        private void FillContent()
        {
            foreach (var upgrade in _upgrades)
            {
                GameObject instance = Instantiate(prefab, _contentsTransform, false);
                UpgradeContent upgradeUI = instance.GetComponent<UpgradeContent>();
                _contents.Add(upgradeUI);
                upgradeUI.Setup(upgrade, controller);
            }
        }
        private void OnDestroy()
        {
            _upgradeContainer.OnUpgradePurchased -= RefreshUI;
        }
    }
}