namespace SharpStore.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SharpStore.Data.SharpStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SharpStore.Data.SharpStoreContext context)
        {
            context.Knives.AddOrUpdate(k => k.Name, new Knife[]
            {
                 new Knife()
                {
                    Name = "Knife 1",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61uxu8TpdSL.png",
                    Price = 50
                },
                new Knife()
                {
                    Name = "Knife 2",
                    ImageUrl = "http://2static.fjcdn.com/pictures/Cs_86123f_5718085.jpg",
                    Price = 150
                },
                new Knife()
                {
                    Name = "Double edged knife",
                    ImageUrl = "https://steamcommunity-a.akamaihd.net/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXH5ApeO4YmlhxYQknCRvCo04DEVlxkKgpovbSsLQJf1fLEcjVL49KJnJm0kfjmNqjFqWle-sBwhtbM8Ij8nVn6_xJvYm7wJ4OUegFsMF_SrFK5lOrohpHutcvPnyQy6HMi4SmOzkCyhAYMMLLPBEUFzQ/256fx255f",
                    Price = 400
                },
                new Knife()
                {
                    Name = "Legendary knife",
                    ImageUrl = "http://csgoclick.com/items/1.png",
                    Price = 800
                },
            });
        }
    }
}
