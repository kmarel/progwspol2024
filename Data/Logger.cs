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
        private CancellationTokenSource state = new CancellationTokenSource();

        private ConcurrentQueue<IBall> ballsQueue;
        private int bufferSize = 32;

        private Logger() 
        {
            ballsQueue = new ConcurrentQueue<IBall>();
            Task.Run(logToFile);
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
                state.Cancel();
            }
        }

        private async void logToFile()
        {
            while(true)
            {
                while(ballsQueue.TryDequeue(out IBall? ball))
                {
                    string fileName = "Derulo.json";
                    string timestamp = DateTime.Now.ToString("HH:mm:ss");
                    string jsonString = JsonSerializer.Serialize(ball);
                    string log = timestamp + ":" + jsonString + "\n";
                    await File.AppendAllTextAsync(fileName, log);
                }
                await Task.Delay(Timeout.Infinite, state.Token).ContinueWith(_ => { });

                if (this.state.IsCancellationRequested)
                {
                    this.state = new CancellationTokenSource();
                }
            }
        }

    }
}
