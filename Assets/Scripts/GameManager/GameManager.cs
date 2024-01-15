using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public enum GameState
    {
        None, Initialized, Playing, Paused, Finished
    }

    public sealed class GameManager : 
        IInitializable,
        ITickable,
        IFixedTickable
    {
        private GameState _currentGameState = GameState.None;
        private GameState _lastGameState = GameState.None;

        private List<Listeners.IGameListener> listeners = new();
        private List<Listeners.IUpdateListener> updateListeners = new();
        private List<Listeners.IFixUpdaterListener> fixUpdateListeners = new();
        private List<Listeners.IPrestartUpdateListener> prestartUpdatelisteners = new();

        public GameManager( GameObject anySceneGO)
        {
            AddListenersFromScene(anySceneGO);
        }

        [Inject]
        public void Construct(List<Listeners.IGameListener> listeners)
        {
            foreach (var listener in listeners)
            {
                AddListener(listener);
            }
        }

        
        internal void AddListenersFromScene(GameObject gameObject)
        {
            foreach (var root in gameObject.scene.GetRootGameObjects())
            {
                AddListeners(root);
            }
        }
        public void AddListeners(GameObject root)
        {
            var listeners = root.GetComponentsInChildren<Listeners.IGameListener>();

            for (int i = 0; i < listeners.Length; i++)
            {
                AddListener(listeners[i]);
            }
        }
        public void AddListener(Listeners.IGameListener newListener)
        {
            listeners.Add(newListener);

            if (newListener is Listeners.IUpdateListener updateListener)
            {
                updateListeners.Add(updateListener);
            }
            if (newListener is Listeners.IFixUpdaterListener fixUpdateListener)
            {
                fixUpdateListeners.Add(fixUpdateListener);
            }
            if (newListener is Listeners.IPrestartUpdateListener lateUpdatelistener)
            {
                prestartUpdatelisteners.Add(lateUpdatelistener);
            }
        }
        public void RemoveListener(Listeners.IGameListener removingListener)
        {
            listeners.Remove(removingListener);

            if (removingListener is Listeners.IUpdateListener updateListener)
            {
                updateListeners.Remove(updateListener);
            }
            if (removingListener is Listeners.IFixUpdaterListener fixUpdateListener)
            {
                fixUpdateListeners.Add(fixUpdateListener);
            }
            if (removingListener is Listeners.IPrestartUpdateListener lateUpdatelistener)
            {
                prestartUpdatelisteners.Add(lateUpdatelistener);
            }
        }

       internal GameState GetCurrentState()
        {
            return _currentGameState;
        }

        public void OnStart()
        {
            if (_currentGameState != GameState.Initialized)
            {
                return;
            }

            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IStartListener startListener)
                {
                    startListener.OnStart();
                }
            }
            _currentGameState = GameState.Playing;
        }
        public void OnFinish()
        {
            if (_currentGameState != GameState.Playing)
            {
                return;
            }

            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IFinishListener finishListener)
                {
                    finishListener.OnFinish();
                }
            }
            _currentGameState = GameState.Finished;

        }
        public void OnPause()
        {
            if (_currentGameState == GameState.Playing || _currentGameState == GameState.Initialized)
            {
                for (int i = 0; i < listeners.Count; i++)
                {
                    if (listeners[i] is Listeners.IPauseListener pausListener)
                    {
                        pausListener.OnPause();
                    }
                }

                _lastGameState = _currentGameState;
                _currentGameState = GameState.Paused;
            }
        }
        public void OnResume()
        {
            if (_currentGameState != GameState.Paused)
            {
                return;
            }

            if (_lastGameState == GameState.Initialized)
            {
                _currentGameState = GameState.Initialized;
            }
            else
            {
                for (int i = 0; i < listeners.Count; i++)
                {
                    if (listeners[i] is Listeners.IResumeListener resumeListener)
                    {
                        resumeListener.OnResume();
                    }
                }
                _currentGameState = GameState.Playing;
            }
        }


        public void Initialize()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IInitListener initListener)
                {
                    initListener.OnInit();
                }
            }
            _currentGameState = GameState.Initialized;
        }
        public void Tick()
        {
            if (_currentGameState == GameState.Initialized)
            {
                PrestartUpdate();
            }


            if (_currentGameState != GameState.Playing)
            {
                return;
            }
            for (int i = 0; i < updateListeners.Count; i++)
            {
                updateListeners[i].OnUpdate(Time.deltaTime);
            }
        }
        public void FixedTick()
        {
            if (_currentGameState != GameState.Playing)
            {
                return;
            }
            for (int i = 0; i < fixUpdateListeners.Count; i++)
            {
                fixUpdateListeners[i].OnFixedUpdate(Time.fixedDeltaTime);
            }
        }
        private void PrestartUpdate()
        {
            for (int i = 0; i < prestartUpdatelisteners.Count; i++)
            {
                prestartUpdatelisteners[i].OnPrestartUpdate(Time.deltaTime);
            }
        }

    }
}