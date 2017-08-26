namespace _13.BrowseCakes
{
    using System.Linq;

    using System;
    using System.IO;

    public class BrowseCakes
    {
        public static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>Browse Cakes</title>\r\n</head>\r\n<body>\r\n<a href=BrowseCakes.exe\">Back to home</a><br><br>\r\n<form action=\"BrowseCakes.exe\" method=\"GET\">\r\n    <input type=\"text\" id=\"name\" name=\"cakeName\">\r\n    <input type=\"submit\" value=\"Search\">\r\n</form>\r\n</body>\r\n</html>");

            string getContent = Environment.GetEnvironmentVariable("QUERY_STRING");
            string cakeName = getContent.Split('=')[1];
            string[] databaseContent = File.ReadAllLines("database.csv");
            var filtered = databaseContent.Where(s => s.Contains(cakeName));
            foreach (var cake in filtered)
            {
                var cakeData = cake.Split(',');
                var name = cakeData[0];
                var price = decimal.Parse(cakeData[1]);
                Console.WriteLine($"<p>{name} ${price}</price>");
            }
        }
    }
}