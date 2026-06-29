using UnityEngine;

namespace Upgrade
{
    
        [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade Data")]
        public class UpgradeData : ScriptableObject
        {
            [SerializeField] private string _name;
            [SerializeField] private string _description;
            [SerializeField] private float _cost;
            [SerializeField] private int _level;
            [SerializeReference] public IEffect Effect;
            public string Name => _name;
            public string Description => _description;
            public float Cost => _cost;
            public int Level => _level;

            public void LevelUp() => _level++;
        }
    
}
