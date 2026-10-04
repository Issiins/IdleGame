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
            StartCoroutine(AnimateText());
        }
        private void RemoveDroplet() => Destroy(gameObject);

        private IEnumerator AnimateText()
        {
            var timeElapsed = 0f;
            Color originalColor = _text.color; 
            var startingPosition = _rectTransform.anchoredPosition.y;
            while (timeElapsed < duration)
            {
                FadeText(originalColor, timeElapsed);
                MoveText(startingPosition, timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        private void FadeText(Color originalColor, float timeElapsed)
        {
            var colorAfterLerp = Color.Lerp(originalColor, Color.clear, timeElapsed/duration);
            _text.color = colorAfterLerp;
        }
        private void MoveText(float startingPosition, float timeElapsed)
        {
            var x = CalculateDistance(timeElapsed);
            var y = CalculateHeight(startingPosition, timeElapsed);
            _rectTransform.anchoredPosition = new Vector2(x, y);
        }
        private float CalculateDistance(float time) => strength * Mathf.Cos(angle * Mathf.Deg2Rad) * time;
        private float CalculateHeight(float startingPos, float time) => startingPos + strength * Mathf.Sin(angle * Mathf.Deg2Rad) * time - 0.5f * gravity * time * time;
    }
}