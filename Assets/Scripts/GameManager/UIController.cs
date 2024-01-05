using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class UIController : MonoBehaviour,
        Listeners.IUpdateListener,
        Listeners.IInitListener,
        Listeners.IFinishListener

    {
        [SerializeField]
        private GameManager _gameManager;
        [SerializeField]
        private UIView _view;
        [SerializeField]
        private int _countdown;

        private float _timer;
        public bool _CanUpdate { get => _canUpdate; set => _canUpdate = value; }
        private bool _canUpdate;

        public void OnInit()
        {
            _timer = 0;
            _canUpdate = true;
            _view.SetInitView();
        }

        public void OnStartClick()
        {
            _view.SetStartView();
            StartCoroutine(Countdown(_countdown));
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canUpdate)
            {
                _timer += deltaTime;
            }
        }

        private IEnumerator Countdown(int secToStart)
        {
            int i = secToStart;
            while (i > 0)
            {
                var needTime = _timer + 1f;
                _view.SetCountdownText(i.ToString());

                while (true)
                {
                    if (_timer >= needTime)
                    {
                        i--;
                        break;
                    }

                    yield return null;
                }
                yield return null;
            }
            _view.SetCountdownText("");

            _gameManager.OnStart(); 
        }

        public void OnFinish()
        {
            _view.ShowEndView();
        }

        public void OnPauseClick()
        {
            _canUpdate = false;
            _view.SetPauseView();

            if (_gameManager.GetCurrentState() == GameState.Inited)
            {
                _CanUpdate = false;
            }
            else
            { _gameManager.OnPause();}
           
        }

        public void OnResumeClick()
        {
            _canUpdate = true;
            _view.SetResumeView();

            if (_gameManager.GetCurrentState() == GameState.Inited)
            {
                _CanUpdate = true;
            }
            else
            {
                _gameManager.OnResume();
            }
        }
    }
}
