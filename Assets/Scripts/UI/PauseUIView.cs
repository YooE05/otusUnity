using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class PauseUIView : MonoBehaviour
    {
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private Button _resumeButton;             

        public void SetPauseView()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(true);
        }

        public void SetResumeView()
        {
            _pauseButton.gameObject.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
        }

        internal void HideView()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
        }
    }
}
