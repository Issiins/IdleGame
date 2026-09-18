using EditorScripts;
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
    }
}