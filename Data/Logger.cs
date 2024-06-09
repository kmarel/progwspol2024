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
        private Thread thread;

        private ConcurrentQueue<BallAndTime> ballsQueue;
        private int bufferSize = 32;

        private StreamWriter logFile;

        private AutoResetEvent logEvent = new AutoResetEvent(false);

        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());

        private Logger() 
        {
            ballsQueue = new ConcurrentQueue<BallAndTime>();
            thread = new Thread(logToFile);
            thread.IsBackground = true;
            thread.Start();
            logFile = new StreamWriter("Derulo.json", false, Encoding.UTF8);
        }

        ~Logger() 
        {
            logFile.Close();
        }

        public static Logger getInstance()
        {
            return instance.Value;
        }

        public void log(BallAndTime ballAndTime)
        {
            lock(this)
            {
                if (ballsQueue.Count < bufferSize)
                {
                    ballsQueue.Enqueue(ballAndTime);
                    logEvent.Set();
                }
            }
        }

        private void logToFile()
        {
            while(true)
            {
                logEvent.WaitOne();
                while (ballsQueue.TryDequeue(out BallAndTime? ballAndTime))
                {
                    string jsonString = JsonSerializer.Serialize(ballAndTime);
                    logFile.WriteLine(jsonString);
                    logFile.Flush();
                }
            }
        }
    }
}
