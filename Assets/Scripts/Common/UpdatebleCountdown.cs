namespace ShootEmUp
{
    public sealed class UpdatebleCountdown : Countdown, Listeners.IUpdateListener
    {
        public void OnUpdate(float deltaTime)
        {
            _timer += deltaTime;

            UpdateTimer();
        }
    }

    public sealed class PrestartUpdatebleCountdown : Countdown, Listeners.IPrestartUpdateListener
    {
        public void OnPrestartUpdate(float deltaTime)
        {
            _timer += deltaTime;

            UpdateTimer();
        }
    }
}
