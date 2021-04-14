namespace Clock
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Clock
    {
        private CancellationTokenSource cancellationToken;

        public Clock()
        {
            CurrentTime = DateTime.UtcNow;
            Logger = (message) => Console.WriteLine(message);
            IsRunning = false;
        }

        public Action<string> Logger { get; set; }

        public DateTime CurrentTime { get; private set; }

        public bool IsRunning { get; set; }

        public void Start(Action<DateTime> output)
        {
            if (IsRunning)
            {
                Logger($"{CurrentTime}: The process is still running");
            }
            else
            {
                cancellationToken = new CancellationTokenSource();
                IsRunning = true;

                Task.Run(() =>
                {
                    while (!cancellationToken.Token.IsCancellationRequested)
                    {
                        CurrentTime = DateTime.UtcNow;
                        Thread.Sleep(1000);
                        output(CurrentTime);
                    }

                    Logger($"{CurrentTime}: Canceled");
                });
            }
        }

        public void Stop()
        {
            cancellationToken.Cancel();
            IsRunning = false;
        }
    }
}