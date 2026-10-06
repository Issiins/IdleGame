using Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

namespace Player.Upgrade.UI
{
    public class UpgradeContent : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text boost;
        [SerializeField] private TMP_Text cost;
        [SerializeField] private Image icon;
        [SerializeField] private Button purchaseButton;
        [SerializeField] private TMP_Text level;
        private UpgradeInstance _upgrade;
        private PointController _pointController;
        private void SetValues(UpgradeInstance upgrade)
        {
            title.text = upgrade.Data.name;
            description.text = upgrade.Data.description;
            boost.text = upgrade.Data.boost;
            icon.sprite = upgrade.Data.icon;
            FormatText(upgrade);
            purchaseButton = GetComponentInChildren<Button>();
            purchaseButton.onClick.AddListener(OnPurchaseClicked);
        }
        private string LevelFormat(byte currLevel, byte maxLevel) => $"{currLevel}/{maxLevel}";
        private string CostFormat(int currCost) => $"Cost: \n{currCost}p";
        private void FormatText(UpgradeInstance upgrade)
        {
            cost.text = CostFormat(upgrade.CurrentCost);
            level.text = LevelFormat(upgrade.Level, upgrade.Data.maxLevel);
        }
        
        public void Setup(UpgradeInstance upgrade, PointController container)
        {
            _upgrade = upgrade;
            _pointController = container;
            SetValues(_upgrade);
            Refresh();
        }
        /// <summary>
        /// Refreshes upgrade UI in the shop
        /// </summary>
        public void Refresh()
        {
            FormatText(_upgrade);
            bool unlocked = _upgrade.IsUnlockable(_pointController.GetUpgrades());
            bool isMaxedLevel = _upgrade.Level == _upgrade.Data.maxLevel;
            gameObject.SetActive(unlocked);
            if(!unlocked) return;
            if(!isMaxedLevel)
                return;
            purchaseButton.interactable = false;
            cost.text = "Upgrade \n MAXED";
            
        }
        private void OnPurchaseClicked()
        {
            _pointController.GetUpgradeContainer().TryPurchase(_upgrade, _pointController);
        }
    }
}