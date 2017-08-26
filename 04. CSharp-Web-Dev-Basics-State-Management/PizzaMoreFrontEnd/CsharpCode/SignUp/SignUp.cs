namespace SignUp
{
    using System;
    using System.IO;

    public class SignUp
    {
        public static void Main()
        {
            Console.WriteLine("Content-type: text/html;\r\n");
            string html = File.ReadAllText("../htdocs/sign-up.html");
            Console.WriteLine(html);
        }
    }
}
