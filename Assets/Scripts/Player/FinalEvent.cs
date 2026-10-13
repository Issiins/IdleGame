using System.Collections;
using Interaction;
using UI;
using UI.WordWaterfall;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Player
{
    public class FinalEvent : MonoBehaviour
    {
        private enum SpriteLevel
        {
            First, Second, Third
        }
        #region Constants 
        private static readonly int[] _clickCountStage = {10,20,30,40 };
        private static readonly int Trigger = Animator.StringToHash("Break");
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        #endregion
        [Header("References")]
        [SerializeField] PointController pointController;
        [SerializeField] private Button clickButton;
        [SerializeField] private GameObject heartPieces;
        [SerializeField] private WordSpawner wordSpawner;
        [SerializeField] private InputController inputController;
        
        [Header("Point system")] 
        [SerializeField] private float pointUpdateDuration;
        [SerializeField] private float min;
        [SerializeField] private float max;
        
        [Header("Visuals and Audio")] 
        [SerializeField] private Animator fadeScreen;
        [SerializeField] private float holdOnSceen;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private AudioClip breakingGlassSFX;
        #region Runtime state
        private int _clickCount = 0;
        private SpriteLevel? _currentLevel;
        private WaitForSeconds _wt;
        private Coroutine _coroutine;
        #endregion
        
        #region Cached components
        private Animator _animator;
        private AudioSource _audioSource;
        private AnimationFinished _breakDone;
        private AnimationFinished _fadeDone;
        #endregion
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
            wordSpawner.SetSpawningStatus(false);
            _breakDone = _animator.GetBehaviour<AnimationFinished>();
            _breakDone.Finished += OnBreakFinished;
            _animator.SetBool(Trigger, true);
        }
        private void OnBreakFinished()
        {
            _breakDone.Finished -= OnBreakFinished;
            _fadeDone = fadeScreen.GetBehaviour<AnimationFinished>();
            _fadeDone.Finished += OnFadeFinished;
            fadeScreen.SetTrigger(FadeIn);
        }
        private void OnFadeFinished()
        {
            _fadeDone.Finished -= OnFadeFinished;
            StartCoroutine(ReturnToMain());
        }
        private IEnumerator ReturnToMain()
        {
            yield return new WaitForSeconds(holdOnSceen);
            inputController.EnableDeviceInput();
            SceneManager.LoadScene("Main");
        }
        
        private void OnDisable()
        {
            pointController.GetUpgradeContainer().FinalEvent -= StartFinalMotion;
            StopAllCoroutines();
        }
    }
}