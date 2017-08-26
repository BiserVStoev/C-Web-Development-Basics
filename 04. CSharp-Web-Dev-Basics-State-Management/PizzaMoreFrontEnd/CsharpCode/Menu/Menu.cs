namespace Menu
{
    using System;
    using System.IO;

    public class Menu
    {
        public static void Main()
        {
            Console.WriteLine("Content-type: text/html;\r\n");
            string html = File.ReadAllText("../htdocs/menu.html");
            Console.WriteLine(html);
        }
    }
}
