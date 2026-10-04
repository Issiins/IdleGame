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
        [Header("Projectile values")] 
        [SerializeField] private float strength;
        [SerializeField] private float gravity;
        [SerializeField] private float angle;
        private RectTransform _rectTransform;
        private TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Invoke(nameof(RemoveDroplet), duration);
            StartCoroutine(TextFade());
            StartCoroutine(MoveText());
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
        private IEnumerator MoveText()
        {
            var timeElapsed = Time.deltaTime;
            var startingPosition = _rectTransform.anchoredPosition.y;
            while (timeElapsed < duration)
            {
                var x = CalculateDistance(timeElapsed);
                var y = CalculateHeight(startingPosition, timeElapsed);
                _rectTransform.anchoredPosition = new Vector2(x, y);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        private float CalculateDistance(float time) => strength * Mathf.Cos(angle * Mathf.Deg2Rad) * time;
        private float CalculateHeight(float startingPos, float time) => startingPos + strength * Mathf.Sin(angle * Mathf.Deg2Rad) * time - 0.5f * gravity * time * time;
    }
}