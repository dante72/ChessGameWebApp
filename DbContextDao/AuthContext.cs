using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;


namespace DbContextDao
{
    public class AuthContext : DbContext
    {
        public DbSet<Account> Users { get; set; }
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;port=9999;user=root;password=qwerty;database=Auth;",
                new MySqlServerVersion(new Version(8, 0, 28))
            );
        }*/
    }
}