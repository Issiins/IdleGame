using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.WordWaterfall
{
    public class WordSpawner : MonoBehaviour
    {
        private Vector2 _spawningRadius; // Spawning radius is based on size of the sprite 
        [SerializeField] private RectTransform spawningBox;
        [SerializeField] private GameObject spawnableObject;
        [SerializeField] private float spawningRate = 5f;
        private bool _spawnActive;
        private Vector3 _spawningPosition;
        private WaitForSeconds _waitTime;
        private Coroutine _spawningRoutine;

        private void Awake()
        {
            _spawningPosition = transform.position;
        }
        private void Start()
        {
            _spawningRadius = spawningBox.rect.size/2;
            _waitTime = new WaitForSeconds(spawningRate);
            _spawningRoutine = StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return _waitTime;
                if(!_spawnActive) continue;
                var x = Random.Range(_spawningPosition.x-_spawningRadius.x, _spawningPosition.x+_spawningRadius.x);
                var y = Random.Range(_spawningPosition.y-_spawningRadius.y, _spawningPosition.y+_spawningRadius.y);
                var spawningPos = new Vector3(x,y, spawningBox.localPosition.z+1);
                Instantiate(spawnableObject, spawningPos, Quaternion.identity,  spawningBox);
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_spawningRoutine);
        }
        public void SetActive(bool currentValue) => _spawnActive = currentValue;

        public void SetSpawnRate(float rate)
        {
            if(Mathf.Approximately(spawningRate, rate)) return;
            spawningRate = rate;
            RestartSpawn();
        }
        private void RestartSpawn()
        {
            StopCoroutine(_spawningRoutine);
            SetWaitTime(spawningRate);
            _spawningRoutine = StartCoroutine(Spawn());
        }
        private void SetWaitTime(float rate) => _waitTime = new WaitForSeconds(rate);
    }
}