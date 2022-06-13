using Microsoft.EntityFrameworkCore;
using Models;

namespace ChessGameWebApp.Server.Repository
{
    public class AppDbContext : DbContext
    {
        private DbSet<Account> Accounts => Set<Account>();
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
