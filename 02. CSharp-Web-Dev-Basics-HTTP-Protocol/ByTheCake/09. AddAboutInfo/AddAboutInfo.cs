namespace _09.AddAboutInfo
{
    using System;

    public class AddAboutInfo
    {
        public static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"description\" content=\"Buy the cake in By the Cake\">\r\n    <meta name=\"keywords\" content=\"cakes, buy\">\r\n    <meta name=\"author\" content=\"Valentin Kolev\">\r\n    <title>By The Cake</title>\r\n</head>\r\n<body>\r\n    <h1>By The Cake</h1>\r\n    <h2>Enjoy our awesome cakes</h2>\r\n    <hr>\r\n    <ul>\r\n        <li><a href=\"#\">Home</a>\r\n            <ol>\r\n                <li><a href=\"#cakes\">Our Cakes</a></li>\r\n                <li><a href=\"#stores\">Our Stores</li></li>\r\n            </ol>\r\n        </li>\r\n        <li><a href=\"AddCake.exe\">Add Cake</a></li></li>\r\n        <li><a href=\"BrowseCakes.exe\">Browse Cakes</a></li>\r\n        <li><a href=\"#about\">About Us</a></li>\r\n    </ul>\r\n    <h2>Home</h2>\r\n    <section>\r\n        <h3 id=\"cakes\">Our Cakes</h3>\r\n        <p>Cake is a form of sweet dessert that is typically baked. In its oldest forms, cakes were modifications of breads, but cakes now cover a wide range of preparations that can be simple or elaborate, and that share features with other desserts such as pastries, meringues, custards, and pies.</p>\r\n        <<img src=\"http://wallpapercave.com/wp/63hB3f3.jpg\" width=\"200\"/>\r\n    </section>\r\n    <section>\r\n        <h3 id=\"stores\">Our Stores</h3>\r\n        <p>Our stores are located in 21 cities all over the world. Come and see what we have for you.</p>\r\n        <img src=\"https://i.ytimg.com/vi/aryWey6TQF0/maxresdefault.jpg\" width=\"200\"/>\r\n    </section>\r\n    <h2 id=\"about\">About Us</h2>\r\n    <dl>\r\n        <dt>By the Cake Ltd.</dt>\r\n        <dd>Company Name</dd>\r\n        <dt>John Smith</dt>\r\n        <dd>Owner</dd>\r\n    </dl>\r\n    <pre style=\"background-color: #F94F80\">\r\n        City: Hong Kong City:   tSalzburg\r\n        Address: ChoCoLad 18    tAddress: SchokoLeiden 73\r\n        Phone: +78952804429     Phone: +49241432990\r\n    </pre>\r\n    <footer>\r\n        <hr/>\r\n        <p style=\"text-align:center\">&copy;All Rights Reserved.</p>\r\n    </footer>\r\n</body>\r\n</html>");
        }
    }
}
