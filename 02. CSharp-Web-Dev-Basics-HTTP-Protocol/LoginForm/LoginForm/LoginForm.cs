namespace LoginForm
{
    using System;

    public class LoginForm
    {
        public static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>Login Form</title>\r\n</head>\r\n<body>\r\n    <form action=\"LoginForm.exe\" method=\"POST\">\r\n        <label for=\"username\">Username:</label>\r\n        <input type=\"text\" name=\"username\" id=\"username\" value=\"Username\"><br>\r\n        <label for=\"password\">Password:</label>\r\n        <input type=\"password\" name=\"username\" id=\"password\" value=\"Password\"><br>\r\n        <input type=\"submit\" value=\"Log in\">\r\n    </form>\r\n</body>\r\n</html>");

            string[] input = Console.ReadLine().Split(new char[] {'&', '='}, StringSplitOptions.RemoveEmptyEntries);
            string username = input[1];
            string password = input[3];
            Console.WriteLine($"Hi {username}, your password is {password}");
        }
    }
}
