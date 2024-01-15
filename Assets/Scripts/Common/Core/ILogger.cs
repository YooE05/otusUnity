
using UnityEngine;

namespace ShootEmUp
{
    public interface ILogger
    {
        public void Log(string message);
    }
    class Logger : ILogger
    {
        public Logger()
        {
          //  Debug.Log("LogVar");
        }
        
        public void Log(string message)
        {
            Debug.Log(message);
        }

    }

}
