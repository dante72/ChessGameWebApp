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
            set
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
        public void Switch(bool off = false)
        {
            if (off)
                TurnOn = false;
            else
                TurnOn = !TurnOn;
        }
        public TimeSpan Value { get => turnOn ? (endTime - DateTime.UtcNow < TimeSpan.Zero ? TimeSpan.Zero : endTime - DateTime.UtcNow) : Delta; }
        private TimeSpan delta = TimeSpan.FromMinutes(30);
        public TimeSpan Delta
        {
            get
            {
                return delta < TimeSpan.Zero ? TimeSpan.Zero : delta; 
            }
            set
            {
                delta = value;
                endTime = DateTime.UtcNow + delta;
            }
        }

        public TimeSpan DeltaNow => endTime - DateTime.UtcNow;
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
