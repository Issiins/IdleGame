using System;
using System.Collections;
using Interaction;
using UI.WordWaterfall;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
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
        [SerializeField] private GameObject heartPieces;
        [SerializeField] private WordSpawner wordSpawner;
        [SerializeField] private InputController inputController;
        
        [Header("Random number range")] 
        [SerializeField] private float min;
        [SerializeField] private float max;
        
        [Header("Other settings")]
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private AudioClip breakingGlassSFX;
        private Animator _animator;
        private static readonly int[] _clickCountStage = {10,20,30,40 };
        private static readonly int Trigger = Animator.StringToHash("Break");
        private int _clickCount = 0;
        private SpriteLevel? _currentLevel;
        private WaitForSeconds _wt;
        private Coroutine _coroutine;
        private AudioSource _audioSource;
        private void Awake()
        {
            pointController.GetUpgradeContainer().FinalEvent += StartFinalMotion;
            _wt = new WaitForSeconds(pointUpdateDuration);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = breakingGlassSFX;
            _animator = heartPieces.GetComponent<Animator>();
            heartPieces.SetActive(false);
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

            SpriteLevel? level;
            switch (_clickCount)
            {
                case var _ when _clickCount >= _clickCountStage[3]:
                    StartEnding();
                    return;
                case var _ when _clickCount >= _clickCountStage[2]:
                    level = SpriteLevel.Third;
                    break;
                case var _ when _clickCount >= _clickCountStage[1]:
                    level = SpriteLevel.Second;
                    break;
                case var _ when _clickCount >= _clickCountStage[0]:
                    level = SpriteLevel.First;
                    break;
                default:
                    level = _currentLevel;
                    break;
            }

            UpdateButtonSprite(level);
        }

        private void UpdateButtonSprite(SpriteLevel? spriteLevel)
        {
            if(_currentLevel == spriteLevel) return;
            _currentLevel = spriteLevel;
            clickButton.image.sprite = _currentLevel switch
            {
                SpriteLevel.First => sprites[(int)SpriteLevel.First],
                SpriteLevel.Second => sprites[(int)SpriteLevel.Second],
                SpriteLevel.Third => sprites[(int)SpriteLevel.Third],
                _ => clickButton.image.sprite
            };
            _audioSource.Play();
        }

        private void StartEnding()
        {
            inputController.DisableDeviceInput();
            clickButton.gameObject.SetActive(false);
            heartPieces.SetActive(true);
            _animator.SetBool(Trigger, true);
            wordSpawner.SetSpawningStatus(false);
            // Add fade to black screen.
            inputController.EnableDeviceInput();
        }
        
        private void OnDisable()
        {
            pointController.GetUpgradeContainer().FinalEvent -= StartFinalMotion;
            if(_coroutine == null) return;
            StopCoroutine(_coroutine);
        }
    }
}