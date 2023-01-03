﻿using ChessGame;

namespace AuthWebAPI.Models
{
    public class TimeUpdater : IChessObservable, IDisposable
    {
        public List<IChessObserver> Observers { get; set; } = new List<IChessObserver>();
        private readonly Timer timer;

        public TimeUpdater()
        {
            timer = new Timer((_) =>
            {
                ((IChessObservable)this).Notify();
            }, null, 0, 1000);
        }

        public void Dispose()
        {
           timer.Dispose();
        }
    }
}
