using TMPro;
using UnityEngine;

namespace UI
{
    public class Message : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float displayTime;
        private void Awake() => HideUI();
        public void ShowMessage(string message)
        {
            CancelInvoke(nameof(HideUI));
            text.text = message;
            gameObject.SetActive(true);
            Invoke(nameof(HideUI), displayTime);
        }
        private void HideUI() => gameObject.SetActive(false);
    }
}