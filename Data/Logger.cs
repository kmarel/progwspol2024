using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal class Logger
    {

        private static Logger? instance;
        private Thread thread;

        private ConcurrentQueue<IBall> ballsQueue;
        private int bufferSize = 32;

        private Logger() 
        {
            ballsQueue = new ConcurrentQueue<IBall>();
            thread = new Thread(logToFile);
            thread.IsBackground = true;
            thread.Start();
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
                while(ballsQueue.TryDequeue(out IBall? ball))
                {
                    string fileName = "Derulo.json";
                    string timestamp = DateTime.Now.ToString("HH:mm:ss");
                    string jsonString = JsonSerializer.Serialize(ball);
                    string logLine = "[" + timestamp + "]: " + jsonString;
                    using (StreamWriter logFile = new StreamWriter(fileName, true))
                    {
                        logFile.WriteLine(logLine);
                    }
                }
            }
        }
    }
}
