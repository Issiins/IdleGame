using System;
using UnityEngine;

namespace UI
{
    public class WordSpawner : MonoBehaviour
    {
        private Vector3 _spawningRadius; // Spawning radius is based on size of the sprite 
        [SerializeField] private float spawningRate;
        private void Awake()
        {
            var sprite = GetComponentInChildren<SpriteRenderer>();
            _spawningRadius = sprite.bounds.size;
            sprite.enabled = false;
        }
    }
}