using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.WordWaterfall
{
    public class WordDroplet : MonoBehaviour
    {
        [SerializeField] private float timeExist;
        [Header("Drop values")] 
        //[SerializeField] private float strength;
        
        private TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            Invoke(nameof(RemoveDroplet), timeExist);
            StartCoroutine(TextFade());
        }
        private void RemoveDroplet() => Destroy(gameObject);

        private IEnumerator TextFade()
        {
            var t = 0f;
            Color originalColor = _text.color; 
            while (t < timeExist)
            {
                t += Time.deltaTime;
                var colorAfterLerp = Color.Lerp(originalColor, Color.clear, t/timeExist);
                _text.color = colorAfterLerp;
                yield return null;
            }
        }
    }
}