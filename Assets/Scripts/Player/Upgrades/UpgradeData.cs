using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Upgrades
{
        /// <summary>
        /// Constant data for upgrade class.
        /// Only mutable data is Level 
        /// </summary>
        [CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade Data")]
        public class UpgradeData : ScriptableObject
        {
            [SerializeField] private Image _picture;
            [SerializeField] private string _name;
            [SerializeField] private string _description;
            [SerializeField] private int _cost;
            [SerializeField] private byte _maxLevel;
            [SerializeField] private float _multiplier;
            [SerializeField] private List<UnlockRequirement>  _unlockRequirements;
            [SerializeReference, SubclassSelector] public IEffect Effect;
            public Image Picture => _picture;
            public List<UnlockRequirement> UnlockRequirements => _unlockRequirements;
            public byte MaxLevel => _maxLevel;
            public string Name => _name;
            public string Description => _description;
            public float Multiplier => _multiplier;
            public int Cost => _cost;
        }
    
}
