namespace SendEmail
{
    using System;

    public class SendEmail
    {
        public static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");

            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>Send Email</title>\r\n</head>\r\n<body>");

            string request = Environment.GetEnvironmentVariable("REQUEST_METHOD").ToLower();

            string validUsername = "suAdmin";
            string validPassword = "aDmInPw17";

            if (request == "get")
            {
                PrintForm();
            }
            else if (request == "post")
            {
                string[] input = Console.ReadLine().Split(new char[] { '=', '&' }, StringSplitOptions.RemoveEmptyEntries);
                string username = input[1];
                string password = input[3];

                if (username.Trim() != validPassword && password.Trim() != validPassword)
                {
                    PrintForm();
                    Console.WriteLine(@"<p style=""color:red;"">Invalid username or password!</p>");
                }
                else
                {
                    PrintPostForm();
                }
            }

            Console.WriteLine("</body>\r\n</html>"); 
        }

        public static void PrintForm()
        {
            Console.WriteLine("<h1>Login</h1>\r\n    <form action=\"SendEmail.exe\" method=\"POST\">\r\n        <label for=\"username\">Username:</label>\r\n        <input type=\"text\" name=\"username\" id=\"username\" value=\"Username\"><br>\r\n        <label for=\"password\">Password:</label>\r\n        <input type=\"text\" name=\"password\" id=\"password\" value=\"Password\"><br>\r\n        <input type=\"submit\" value=\"Log In\">\r\n    </form>");
        }

        public static void PrintPostForm()
        {
            Console.WriteLine("<h1>Hello suAdmin!</h1>\r\n    <form action=\"\" method=\"post\">\r\n        <label for=\"\">To: </label>\r\n        <input type=\"text\" >\r\n        <br>\r\n        <label for=\"\">Subject: </label>\r\n        <input type=\"text\">\r\n        <br>\r\n        <label for=\"\">Message: </label>\r\n        <input type=\"text\">\r\n        <br>\r\n        <textarea name=\"\" id=\"\" cols=\"30\" rows=\"10\"></textarea>\r\n        <br>\r\n        <input type=\"submit\" value=\"Send\">\r\n    </form>");
        }
    }
}
