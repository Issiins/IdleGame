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
            [field: SerializeField] public Sprite icon { get; private set; }
            [field: SerializeField] public new string name { get; private set; }
            [field: SerializeField] public new string description  { get; private set; }
            [field: SerializeField] public int cost { get; private set; }
            [field: SerializeField] public byte maxLevel { get; private set; }
            [field: SerializeField] public float multiplier { get; private set; }
            [field: SerializeField] public List<UnlockRequirement>  unlockRequirements { get; private set; }
            [SerializeReference, SubclassSelector] public IEffect effect;
            public bool IsValid() =>
                effect != null &&
                !string.IsNullOrEmpty(name) &&
                multiplier > 0 &&
                maxLevel > 0 &&
                cost > 0;
        }
    
}
