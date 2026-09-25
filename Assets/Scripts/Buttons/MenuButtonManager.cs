using EditorScripts;
using Interaction;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Buttons
{
    public class MenuButtonManager : MonoBehaviour
    {
        [SerializeField] private SceneReference playScene;
        private void OnValidate()
        {
            #if UNITY_EDITOR
            playScene.Validate();
            #endif
        }

        public void OnPressQuitButton()
        {
            Application.Quit();
        }

        public void OnPressPlayButton()
        {
            SceneManager.LoadScene(playScene.name);
        }
        public void OnPressLoadButton()
        {
            SceneManager.sceneLoaded += OnPlaySceneLoaded;
            OnPressPlayButton();
        }
        private void OnPlaySceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnPlaySceneLoaded;
            PointController pointController = FindAnyObjectByType<PointController>();
            Message message = FindAnyObjectByType<Message>();
            if (pointController == null)
                return;
            InOut io = new InOut(pointController, pointController.GetUpgradeContainer(), message);
            io.Load();
        }
    }
}