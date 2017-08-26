namespace _11.AddCake
{
    using System;

    public class AddCake
    {
        public static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>AddCake</title>\r\n</head>\r\n<body>\r\n    <form action=\"AddCake.exe\" method=\"post\">\r\n        Name: <input type=\"text\" name=\"name\">\r\n        Price: <input type=\"text\" name=\"Price\">\r\n        <input type=\"submit\" value=\"Add Cake\">\r\n    </form>\r\n</body>\r\n</html>");
            string input = Console.ReadLine();

            if (input != null)
            {
                string[] data = input.Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
                string name = data[1].Replace("+", " ");
                decimal price = decimal.Parse(data[3]);
                var cake = new Cake()
                {
                    Name = name,
                    Price = price
                };

                Console.WriteLine($"name:{cake.Name}");
                Console.WriteLine("</br>");
                Console.WriteLine($"Price:{cake.Price}");
            }
        }
    }
}
