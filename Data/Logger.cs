using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Logger
    {

        private static Logger instance;
        private Thread thread;

        private ConcurrentQueue<IBall> ballsQueue;
        private int bufferSize = 32;

        private Logger() 
        {
            ballsQueue = new ConcurrentQueue<IBall>();
            thread = new Thread(logToFile);
        }

        public static Logger getInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }

        public void log(IBall ballToLog)
        {
            if(ballsQueue.Count < bufferSize) 
            {
                ballsQueue.Enqueue(ballToLog);
            }
        }

        private void logToFile()
        {
            while(true)
            {
                while(ballsQueue.TryDequeue(out IBall ball)) 
                {
                    
                }
            }
        }

    }
}
