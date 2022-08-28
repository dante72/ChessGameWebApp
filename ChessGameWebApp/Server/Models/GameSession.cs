using ChessGame;

namespace ChessGameWebApp.Server.Models
{
    public class GameSession
    {
        public List<Player> Players { get; set; }
        public ChessBoard Board { get; set; }
    }
}
