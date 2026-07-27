using System.Collections.Generic;
using UnityEngine;

namespace Upgrade
{
        /// <summary>
        /// Constant data for upgrade class.
        /// Only mutable data is Level 
        /// </summary>
        [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade Data")]
        public class UpgradeData : ScriptableObject
        {
            [SerializeField] private string _name;
            [SerializeField] private string _description;
            [SerializeField] private float _cost;
            [SerializeField] private int _level; // level - 0, means not purchased 
            [SerializeField] private List<UnlockRequirement>  _unlockRequirements;
            [SerializeReference] public IEffect Effect;
            public List<UnlockRequirement> UnlockRequirements => _unlockRequirements;
            public string Name => _name;
            public string Description => _description;
            public float Cost => _cost;
            public int Level => _level;

            public void LevelUp() => _level++;
        }
    
}
