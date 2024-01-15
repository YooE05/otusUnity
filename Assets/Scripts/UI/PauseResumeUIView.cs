using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class PauseResumeUIView : MonoBehaviour
    {
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private Button _resumeButton;
        [SerializeField]
        private Button _startButton;

        public void AddButtonActions(Action pauseAction, Action resumeAction)
        {
            UnityAction pauseUnityAction = new UnityAction(pauseAction);
            _pauseButton.onClick.AddListener(pauseUnityAction);

            UnityAction resumeUnityAction = new UnityAction(resumeAction);
            _resumeButton.onClick.AddListener(resumeUnityAction);

            _startButton.onClick.AddListener(SetResumeView);
        }
        public void RemoveButtonActions()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
        }


        public void SetPauseView()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(true);
        }
        public void SetResumeView()
        {
            _pauseButton.gameObject.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
        }

        internal void HideView()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
        }
    }
}
