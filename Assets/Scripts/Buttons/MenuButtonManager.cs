using System;
using Interaction;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Buttons
{
    public class MenuButtonManager : MonoBehaviour
    {
        [SerializeField] private string playScene;

        public void OnPressQuitButton()
        {
            Application.Quit();
        }

        public void OnPressPlayButton()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();               
            SceneManager.LoadScene(playScene);
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
            Message message = FindAnyObjectByType<Message>(FindObjectsInactive.Include);
            if (pointController == null || message == null)
                return;
            InOut io = new InOut(pointController, pointController.GetUpgradeContainer(), message);
            io.Load();
        }
    }
}