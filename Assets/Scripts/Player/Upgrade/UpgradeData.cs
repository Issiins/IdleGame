using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Upgrade
{
        /// <summary>
        /// Constant data for upgrade class.
        /// Only mutable data is Level 
        /// </summary>
        [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade Data")]
        public class UpgradeData : ScriptableObject
        {
            [SerializeField] private Sprite _picture;
            [SerializeField] private string _name;
            [SerializeField] private string _description;
            [SerializeField] private float _cost;
            [SerializeField] private byte _maxLevel;
            [SerializeField] private List<UnlockRequirement>  _unlockRequirements;
            [SerializeReference, SubclassSelector] public IEffect Effect;
            public Sprite Picture => _picture;
            public List<UnlockRequirement> UnlockRequirements => _unlockRequirements;
            public string Name => _name;
            public string Description => _description;
            public float Cost => _cost;
        }
    
}
