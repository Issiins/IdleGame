using System;
using UnityEngine;

namespace UI.WordWaterfall
{
    public class WordDroplet : MonoBehaviour
    {
        [SerializeField] private float timeExist;

        private void Start()
        {
            Invoke(nameof(RemoveDroplet), timeExist);
        }
        private void RemoveDroplet() => Destroy(gameObject);
        
    }
    
    
}