using ChessGame;

namespace ChessGameWebApp.Client.Models
{
    public class TimeUpdater : IChessObservable, IDisposable
    {
        public List<IChessObserver> Observers { get; set; } = new List<IChessObserver>();
        private readonly Timer timer;

        public TimeUpdater()
        {
            timer = new Timer((_) =>
            {
                Observers.ForEach(o => o.Update());
            }, null, 0, 1000);
        }

        public void Dispose()
        {
           timer.Dispose();
        }
    }
}
