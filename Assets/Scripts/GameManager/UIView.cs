using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

namespace ShootEmUp
{
    public class UIView : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private Button _resumeButton;
        [SerializeField]
        private TextMeshProUGUI _txtView;

        public void SetInitView()
        {
            _startButton.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
            _txtView.text = "";
        }

        internal void ShowEndView()
        {
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
            _txtView.text = "Game Over";
        }

        public void SetStartView()
        {
            _startButton.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
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

        public void SetCountdownText(string txt)
        {
            _txtView.text = txt;
        }
    }
}
