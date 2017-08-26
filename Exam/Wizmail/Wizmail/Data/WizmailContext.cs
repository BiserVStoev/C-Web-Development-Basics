namespace Wizmail.Data
{
    using System.Data.Entity;
    using Wizmail.Models;

    public class WizmailContext : DbContext
    {
        public WizmailContext()
            : base("name=WizmailContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Login> Logins { get; set; }
    }
}