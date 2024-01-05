using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp
{
    public class Listeners
    {
        public interface IGameListener
        {

        }

        public interface IInitListener : IGameListener
        {
            void OnInit();
        }

        public interface IStartListener : IGameListener
        {
            void OnStart();
        }
        public interface IFinishListener : IGameListener
        {
            void OnFinish();
        }
        public interface IPauseListener : IGameListener
        {
            void OnPause();
        }
        public interface IResumeListener : IGameListener
        {
            void OnResume();
        }
        public interface IUpdateListener : IGameListener
        {
            bool _CanUpdate { get; set; }
            void OnUpdate(float deltaTime);
        }
        public interface IFixUpdaterListener : IGameListener
        {
            bool _CanUpdate { get; set; }
            void OnFixedUpdate(float deltaTime);
        }
        public interface ILateUpdateListener : IGameListener
        {
            bool _CanUpdate { get; set; }
            void OnLateUpdate(float deltaTime);
        }


    }
}