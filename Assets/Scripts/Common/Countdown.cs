using System;

namespace ShootEmUp
{
    abstract class Countdown : IDisposable
    {
        public Action<int> OnValueChanged;
        public Action<Countdown> OnCountdownEnded;

        protected float _timer = 0;
        private float _nextStepTime;
        private int _countOfSteps;
        private float _delay;

        private bool _isCounting = false;

        protected void UpdateTimer()
        {
            if (_isCounting)
            {
                if (_timer >= _nextStepTime)
                {
                    _nextStepTime = _timer + _delay;
                    _countOfSteps--;

                    OnValueChanged?.Invoke(_countOfSteps);

                    if (_countOfSteps == 0)
                    {
                        _isCounting = false;
                        OnCountdownEnded?.Invoke(this);
                    }
                }
            }
        }

        public void StartTimer(int countOfSteps, float delay)
        {
            _delay = delay;
            _nextStepTime = _timer + _delay;
            _countOfSteps = countOfSteps;

            if (_countOfSteps <= 0)
            {
                OnCountdownEnded?.Invoke(this);
            }
            else
            {
                OnValueChanged?.Invoke(countOfSteps);
                _isCounting = true;
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
