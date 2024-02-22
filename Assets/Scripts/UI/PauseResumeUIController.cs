namespace ShootEmUp
{
    public sealed class PauseResumeUIController :
        Listeners.IInitListener,
        Listeners.IFinishListener
    {
        private readonly GameManager _gameManager;
        private readonly PauseResumeUIView _view;

        public PauseResumeUIController(GameManager gameManager, PauseResumeUIView view)
        {
            _gameManager = gameManager;
            _view = view;
            _view.AddButtonActions(OnPauseClick, OnResumeClick);
        }

        public void OnInit()
        {
            _view.HideView();
        }

        public void OnFinish()
        {
            _view.HideView();
            _view.RemoveButtonActions();
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
