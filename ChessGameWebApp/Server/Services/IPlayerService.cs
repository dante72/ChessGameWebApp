﻿using ChessGameWebApp.Server.Models;

namespace ChessGameWebApp.Server.Services
{
    public interface IPlayerService
    {
        Task Add(Player player);
        Task Remove(Player player);
        Task<int> Count();
    }
}