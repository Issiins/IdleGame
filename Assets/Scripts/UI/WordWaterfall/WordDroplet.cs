using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.WordWaterfall
{
    public class WordDroplet : MonoBehaviour
    {
        [SerializeField] private float duration;
        [Header("Drop values")] 
        //[SerializeField] private float strength;
        
        private TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            Invoke(nameof(RemoveDroplet), duration);
            StartCoroutine(TextFade());
        }
        private void RemoveDroplet() => Destroy(gameObject);

        private IEnumerator TextFade()
        {
            var timeElapsed = 0f;
            Color originalColor = _text.color; 
            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                var colorAfterLerp = Color.Lerp(originalColor, Color.clear, timeElapsed/duration);
                _text.color = colorAfterLerp;
                yield return null;
            }
        }
    }
}