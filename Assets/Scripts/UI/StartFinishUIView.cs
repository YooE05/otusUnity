using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShootEmUp
{
    public class StartFinishUIView : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private TextMeshProUGUI _txtView;

        public void ShowStartButton()
        {
            _startButton.gameObject.SetActive(true);
            _txtView.text = "";
        }

        internal void ShowEndText()
        {
            _txtView.text = "Game Over";
        }

        public void SetStartView()
        {
            _startButton.gameObject.SetActive(false);
        }

        public void SetCountdownText(string txt)
        {
            _txtView.text = txt;
        }
    }
}