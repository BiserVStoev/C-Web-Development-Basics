using System.IO;

namespace _12.WriteData
{
    using System;

    public class WriteData
    {
        public static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>AddCake</title>\r\n</head>\r\n<body>\r\n    <a href=ByTheCake.exe\">Back to home</a><br><br>\r\n    <form action=\"AddCake.exe\" method=\"POST\">\r\n        <label for=\"name\">Name: </label>\r\n        <input type=\"text\" id=\"name\" name=\"cakeName\">\r\n        <label for=\"price\">Price: </label>\r\n        <input type=\"text\" id=\"price\" name=\"price\">\r\n        <input type=\"submit\" value=\"Add Cake\">\r\n    </form>\r\n</body>\r\n</html>");
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


                string appended = cake.Name + "," + cake.Price;
                File.AppendAllText("database.csv", appended);
            }
        }
    }
}
