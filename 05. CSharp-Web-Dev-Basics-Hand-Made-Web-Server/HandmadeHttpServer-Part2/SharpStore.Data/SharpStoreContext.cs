namespace SharpStore.Data
{
    using System.Data.Entity;
    using Models;

    public class SharpStoreContext : DbContext
    {
        public SharpStoreContext()
            : base("name=SharpStoreContext")
        {
        }
        

        public DbSet<Knife> Knives { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}