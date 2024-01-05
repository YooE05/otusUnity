using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace ShootEmUp
{

    public enum GameState
    {
        Inited, Started, Paused, Resumed, Finished
    }

    public sealed class GameManager: MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private GameState _gameState;

        private List<Listeners.IGameListener> listeners = new();
        private List<Listeners.IUpdateListener> updateListeners = new();
        private List<Listeners.IFixUpdaterListener> fixUpdateListeners = new();
        private List<Listeners.ILateUpdateListener> lateUpdatelisteners = new();

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
            if (newListener is Listeners.ILateUpdateListener lateUpdatelistener)
            {
                lateUpdatelisteners.Add(lateUpdatelistener);
            }
        }


        public void OnStart()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IStartListener startListener)
                {
                    startListener.OnStart();
                }
            }
            _gameState = GameState.Started;
        }

        public void OnFinish()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IFinishListener finishListener)
                {
                    finishListener.OnFinish();
                }
            }
            _gameState = GameState.Finished;

        }

        internal GameState GetCurrentState()
        {
            return _gameState;
        }

        public void OnPause()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IPauseListener pausListener)
                {
                    pausListener.OnPause();
                }
            }

            _gameState = GameState.Paused;
        }
        public void OnResume()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IResumeListener resumeListener)
                {
                    resumeListener.OnResume();
                }
            }
            _gameState = GameState.Resumed;
        }

      
        private void Start()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i] is Listeners.IInitListener initListener)
                {
                    initListener.OnInit();
                }
            }
            _gameState = GameState.Inited;
        }
        private void Update()
        {
            for (int i = 0; i < updateListeners.Count; i++)
            {
                updateListeners[i].OnUpdate(Time.deltaTime);
            }
        }
        private void FixedUpdate()
        {
            for (int i = 0; i < fixUpdateListeners.Count; i++)
            {
                fixUpdateListeners[i].OnFixedUpdate(Time.fixedDeltaTime);
            }
        }
        private void LateUpdate()
        {
            for (int i = 0; i < lateUpdatelisteners.Count; i++)
            {
                lateUpdatelisteners[i].OnLateUpdate(Time.deltaTime);
            }
        }
    }
}