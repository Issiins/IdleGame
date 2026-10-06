using System.Collections;
using TMPro;
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
        private readonly string[] _wordList = { "Pain", "Alone", "Lonely", "Nobody"};
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
            var startingPosition = _rectTransform.anchoredPosition;
            _text.text = _wordList[Random.Range(0, _wordList.Length)]; // Selects random word from the list
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
        private void MoveText(Vector2 startingPosition, float timeElapsed)
        {
            var xDelta = CalculateDistance(timeElapsed);
            var yDelta = CalculateHeight(startingPosition.y, timeElapsed);
            var direction = Mathf.Sign(startingPosition.x);
            _rectTransform.anchoredPosition = new Vector2(startingPosition.x+direction*xDelta, yDelta);
        }
        
        // Both functions use position over time formulas with launch angle and initial speed
        
        private float CalculateDistance(float time) => strength * Mathf.Cos(angle * Mathf.Deg2Rad) * time;
        private float CalculateHeight(float startingPos, float time) => startingPos + strength * Mathf.Sin(angle * Mathf.Deg2Rad) * time - 0.5f * gravity * time * time;
    }
}