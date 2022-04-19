using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Hangman.Models
{
    public class Timer
    {
        private readonly DispatcherTimer internalTimer;

        public Timer()
        {
            internalTimer = new DispatcherTimer();
        }

        public bool IsEnabled
        {
            get => internalTimer.IsEnabled;
            set => internalTimer.IsEnabled = value;
        }

        public TimeSpan Interval
        {
            get => internalTimer.Interval;
            set => internalTimer.Interval = value;
        }

        public void Start() => internalTimer.Start();
        public void Stop() => internalTimer.Stop();

        public event EventHandler Tick
        {
            add => internalTimer.Tick += value;
            remove => internalTimer.Tick -= value;
        }
    }
}
