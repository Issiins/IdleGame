using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class Heartbeat : MonoBehaviour
    {
        [SerializeField, Min(0)] private float pauseDuration;
        [SerializeField, Min(0)] private float pulseDuration;
        [SerializeField, Min(0)] private float sizeMultiplier;
        [SerializeField] private AudioClip audioClip;
        private AudioSource _audioClipSource;
        private Vector3 _baseScale;
        private Coroutine _heartBeatCoroutine;
        private WaitForSeconds _wt;

        private void Awake()
        {
            _baseScale = transform.localScale;
            _wt = new WaitForSeconds(pauseDuration);
            _audioClipSource = GetComponent<AudioSource>();
            _audioClipSource.clip = audioClip;
            _heartBeatCoroutine = StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            while (true)
            {
                _audioClipSource.Play();
                yield return ScaleTo(_baseScale*sizeMultiplier, pulseDuration);
                yield return ScaleTo(_baseScale, pulseDuration);
                yield return _wt;
            }
        }

        private IEnumerator ScaleTo(Vector3 targetScale, float duration)
        {
            for (var time = 0f; time < duration; time += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, time / duration);
                yield return null;
            }
            transform.localScale = targetScale; // safety line to reach desired scale (Lerp can end before reaching the limit)
        }
        private void OnDisable()
        {
            if(_heartBeatCoroutine != null)
                StopCoroutine(_heartBeatCoroutine);
            transform.localScale = _baseScale;
        }

        public void SetPauseDuration(float duration)
        {
            if(Mathf.Approximately(pauseDuration, duration)) return;
            pauseDuration = duration;
            ResetAnimation();
        }

        private void ResetAnimation()
        {
            OnDisable();
            SetWaitTime(pauseDuration);
            _heartBeatCoroutine = StartCoroutine(Animate());
        }
        private void SetWaitTime(float rate) => _wt = new WaitForSeconds(rate);
        public void SetPulseDuration (float duration) => pulseDuration = duration;
    }
}