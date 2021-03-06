namespace Shouter.Data
{
    using System.Data.Entity;
    using Contracts;
    using Models;

    public class ShouterContext : DbContext, IShouterContext
    {
        public ShouterContext()
            : base("name=ShouterContext")
        {
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Shout> Shouts { get; set; }
        public IDbSet<Login> Logins { get; set; }
        public IDbSet<Notification> Notifications { get; set; }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public DbContext DbContext => this;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(m => m.Following)
            .WithMany()
            .Map(m => m.ToTable("Followers"));
            base.OnModelCreating(modelBuilder);
        }
    }
}