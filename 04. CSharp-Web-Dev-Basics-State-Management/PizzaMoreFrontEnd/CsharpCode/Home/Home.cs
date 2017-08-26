namespace Home
{
    using System;
    using System.IO;
    using System.Text;

    public class Home
    {
        public static void Main()
        {
            Console.WriteLine("Content-type: text/html;\r\n");
            string html = File.ReadAllText("../htdocs/home.html", Encoding.Default);
            Console.WriteLine(html);
        }
    }
}
