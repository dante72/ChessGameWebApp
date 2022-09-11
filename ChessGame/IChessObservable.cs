using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public interface IChessObservable
    {
        protected List<IChessObserver> Observers { get; set; }
        public void Notify()
        {
            Observers.ForEach(o => o.Update());
        }

        public void Subscribe(IChessObserver observer)
        {
            Observers.Add(observer);
        }

        public void Remove(IChessObserver observer)
        {
            Observers.Remove(observer);
        }
    }
}
