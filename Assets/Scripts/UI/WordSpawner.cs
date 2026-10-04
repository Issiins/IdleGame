using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class WordSpawner : MonoBehaviour
    {
        private Vector2 _spawningRadius; // Spawning radius is based on size of the sprite 
        [SerializeField] private RectTransform spawningBox;
        [SerializeField] private GameObject spawnableObject;
        [SerializeField] private float spawningRate = 5f;
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
                var x = Random.Range(_spawningPosition.x-_spawningRadius.x, _spawningPosition.x+_spawningRadius.x);
                var y = Random.Range(_spawningPosition.y-_spawningRadius.y, _spawningPosition.y+_spawningRadius.y);
                var spawningPos = new Vector3(x,y, spawningBox.localPosition.z+1);
                Instantiate(spawnableObject, spawningPos, Quaternion.identity,  spawningBox);
                yield return _waitTime;
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_spawningRoutine);
        }
    }
}