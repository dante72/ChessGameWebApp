global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Models
{
    
    [Table("t_accounts")]
    [Index(nameof(Email), IsUnique = true)]
    public class Account : IEntity
    {
        public Account(int id, string login, string email, string hashPassword)
        {
            Id = id;
            Login = login;
            Email = email;
            HashPassword = hashPassword;
            IsBanned = false;
        }

        public Account()
        {
            Id = 0;
        }

        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Column("username")]
        [MaxLength(25)]
        public string Username { get; set; }

        [Column("login")]
        [MaxLength(25)]
        public string Login { get; set; }

        [Column("password")]
        [MaxLength(255)]
        public string HashPassword { get; set; }

        [DefaultValue("false")]
        [Column("banned")]
        public bool IsBanned { get; set; }

    }
}
