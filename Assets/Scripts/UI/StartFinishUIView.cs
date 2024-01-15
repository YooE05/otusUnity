using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace ShootEmUp
{
    public class StartFinishUIView : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private TextMeshProUGUI _txtView;

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
            _txtView.text = "";
        }

        internal void ShowEndText()
        {
            _txtView.text = "Game Over";
        }

        public void HideStartButton()
        {
            _startButton.gameObject.SetActive(false);
        }

        public void SetCountdownText(string txt)
        {
            _txtView.text = txt;
        }
    }
}