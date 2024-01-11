using UnityEngine;

namespace ShootEmUp
{
    public class PauseUIController : MonoBehaviour,
        Listeners.IInitListener,
        Listeners.IFinishListener
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private PauseUIView _view;

        public void OnInit()
        {
            _view.HideView();
        }

        public void ShowPauseButton()
        {
            _view.SetResumeView();
        }

        public void OnFinish()
        {
            _view.HideView();
        }

        public void OnPauseClick()
        {
            _view.SetPauseView();
            _gameManager.OnPause();

        }

        public void OnResumeClick()
        {
            _view.SetResumeView();
            _gameManager.OnResume();
        }
    }
}
