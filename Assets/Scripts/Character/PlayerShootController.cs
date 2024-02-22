namespace ShootEmUp
{
    public sealed class PlayerShootController :
        Listeners.IStartListener,
        Listeners.IFinishListener
    {
        private readonly WeaponController _playerWeaponController;
        private readonly InputManager _inputManager;

        public PlayerShootController(WeaponController playerWeaponController, InputManager inputManager)
        {
            _playerWeaponController = playerWeaponController;
            _inputManager = inputManager;
        }

        public void OnStart()
        {
            _inputManager.OnFireButtonPressed += Shoot;
        }

        public void OnFinish()
        {
            _inputManager.OnFireButtonPressed -= Shoot;
        }

        private void Shoot()
        {
            _playerWeaponController.ShootStraight();
        }
    }
}