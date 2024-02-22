namespace ShootEmUp
{
    public sealed class StartFinishUIController :
        Listeners.IInitListener,
        Listeners.IFinishListener
    {
        private readonly GameManager _gameManager;
        private readonly StartFinishUIView _view;
        private readonly StartGameConfig _startGameConfig;

        private PrestartUpdatebleCountdown _countdown = new();

        public StartFinishUIController(GameManager gameManager, StartFinishUIView view, StartGameConfig startGameConfig)
        {
            _gameManager = gameManager;
            _startGameConfig = startGameConfig;
            _view = view;
            _view.AddButtonActions(OnStartClick);
        }

        public void OnInit()
        {
            _view.ShowStartButton();

            _countdown.OnValueChanged += ChangeTimerText;
            _countdown.OnCountdownEnded += RemoveCountdownAndStart;
            _gameManager.AddListener(_countdown);
        }

        public void OnStartClick()
        {
            _view.HideStartButton();

            if (_startGameConfig.IsDelayedStart)
            {
                _countdown.StartTimer(_startGameConfig.CountdownTime, 1);
            }
            else
            {
                StartGame();
            }

        }

        private void ChangeTimerText(int newTxt)
        {
            _view.SetCountdownText(newTxt.ToString());
        }

        private void RemoveCountdownAndStart(Countdown cd)
        {
            _countdown.OnValueChanged -= ChangeTimerText;
            _countdown.OnCountdownEnded -= RemoveCountdownAndStart;
            _gameManager.RemoveListener(_countdown);
            StartGame();
        }

        private void StartGame()
        {
            _view.SetCountdownText(string.Empty);
            _gameManager.OnStart();
        }

        public void OnFinish()
        {
            _view.RemoveButtonActions();
            _view.ShowEndText();
        }
    }
}