using UnityEngine;


namespace ShootEmUp
{
    public class StartFinishUIController : MonoBehaviour,
        Listeners.IInitListener,
        Listeners.IFinishListener
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private StartFinishUIView _view;

        [SerializeField]
        private int _countdownTime;

        private PrestartUpdatebleCountdown _countdown = new();

        public void OnInit()
        {
            _view.ShowStartButton();

            _countdown.OnValueChanged += ChangeTimerText;
            _countdown.OnCountdownEnded += StartGame;
            _gameManager.AddListener(_countdown);
        }

        public void OnStartClick()
        {
            _view.SetStartView();
            _countdown.StartTimer(_countdownTime, 1);
        }

        private void ChangeTimerText(int newTxt)
        {
            _view.SetCountdownText(newTxt.ToString());
        }

        private void StartGame(Countdown cd)
        {
            _countdown.OnValueChanged -= ChangeTimerText;
            _countdown.OnCountdownEnded -= StartGame;
            _gameManager.RemoveListener(_countdown);
            _countdown.Dispose();

            _view.SetCountdownText(string.Empty);

            _gameManager.OnStart();
        }

        public void OnFinish()
        {
            _view.ShowEndText();
        }
    }
}