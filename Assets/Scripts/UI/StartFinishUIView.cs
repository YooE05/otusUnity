using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace ShootEmUp
{
    public sealed class StartFinishUIView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        [SerializeField] private TextMeshProUGUI _textView;

        public void AddButtonActions(System.Action startAction)
        {
            UnityAction startUnityAction = new UnityAction(startAction);
            _startButton.onClick.AddListener(startUnityAction);
        }

        public void RemoveButtonActions()
        {
            _startButton.onClick.RemoveAllListeners();
        }

        public void ShowStartButton()
        {
            _startButton.gameObject.SetActive(true);
            _textView.text = "";
        }

        internal void ShowEndText()
        {
            _textView.text = "Game Over";
        }

        public void HideStartButton()
        {
            _startButton.gameObject.SetActive(false);
        }

        public void SetCountdownText(string txt)
        {
            _textView.text = txt;
        }
    }
}