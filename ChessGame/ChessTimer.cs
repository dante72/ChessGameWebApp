using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ChessTimer
    {
        private bool turnOn = false;
        public bool TurnOn {
            private set
            {
                turnOn = value;
                if (turnOn)
                    Start();
                else
                    Stop();
            }
            get
            {
                return turnOn;
            }
        }

        public void Switch() => TurnOn = !TurnOn;
        public TimeSpan Value { get => turnOn ? (endTime - DateTime.UtcNow < TimeSpan.Zero ? TimeSpan.Zero : endTime - DateTime.UtcNow) : Delta; }
        private TimeSpan delta = TimeSpan.Zero;
        public TimeSpan Delta
        {
            get => delta < TimeSpan.Zero ? TimeSpan.Zero : delta;
            set
            {
                delta = value;
                endTime = DateTime.UtcNow + delta;
            }
        }
        private DateTime endTime;
        private void Stop()
        {
            Delta = endTime - DateTime.UtcNow;
        }
        private void Start()
        {
            endTime = DateTime.UtcNow + Delta;
        }
    }
}
