using System;
using System.Collections;
using Interaction;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class FinalEvent : MonoBehaviour
    {
        private enum SpriteLevel
        {
            First, Second, Third
        }
        
        [SerializeField] PointController pointController;
        [SerializeField] private float pointUpdateDuration;
        [SerializeField] private Button clickButton;
        
        [Header("Random number range")] 
        [SerializeField] private float min;
        [SerializeField] private float max;
        
        [Header("Other settings")]
        [SerializeField] private Sprite[] sprites;

        private int _clickCount = 0;
        private SpriteLevel _currentLevel;
        private WaitForSeconds _wt;
        private Coroutine _coroutine;
        private void Awake()
        {
            pointController.GetUpgradeContainer().FinalEvent += StartFinalMotion;
            _wt = new WaitForSeconds(pointUpdateDuration);
        }

        private void StartFinalMotion()
        {
            pointController.ChangeRunningStatus(false);
            _coroutine = StartCoroutine(ShowRandomNumbers());
            ChangeButtonListener();
        }

        private IEnumerator ShowRandomNumbers()
        {
            while (true)
            {
                float randomNumber = Random.Range(min, max);
                pointController.GetPointUIController().UpdateValue(randomNumber);
                yield return _wt; 
            }
        }

        private void ChangeButtonListener()
        {
            clickButton.onClick.RemoveAllListeners();
            clickButton.onClick.AddListener(ClickEvent);
        }

        private void ClickEvent()
        {
            _clickCount++;
            
            
        }

        private void UpdateButtonSprite(SpriteLevel spriteLevel)
        {
            if(_currentLevel == spriteLevel) return;
            clickButton.image.sprite = _currentLevel switch
            {
                SpriteLevel.First => sprites[(int)SpriteLevel.First],
                SpriteLevel.Second => sprites[(int)SpriteLevel.Second],
                SpriteLevel.Third => sprites[(int)SpriteLevel.Third],
                _ => clickButton.image.sprite
            };
        }

        
        private void OnDisable()
        {
            pointController.GetUpgradeContainer().FinalEvent -= StartFinalMotion;
            if(_coroutine == null) return;
            StopCoroutine(_coroutine);
        }
    }
}