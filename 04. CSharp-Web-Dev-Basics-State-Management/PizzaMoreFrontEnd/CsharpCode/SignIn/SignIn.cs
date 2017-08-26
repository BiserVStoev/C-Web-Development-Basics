namespace SignIn
{
    using System;
    using System.IO;

    public class SignIn
    {
        public static void Main()
        {
            Console.WriteLine("Content-type: text/html;\r\n");
            string html = File.ReadAllText("../htdocs/sign-in.html");
            Console.WriteLine(html);
        }
    }
}
