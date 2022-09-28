using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWebApp.Shared
{
    public class ChatMessage
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
