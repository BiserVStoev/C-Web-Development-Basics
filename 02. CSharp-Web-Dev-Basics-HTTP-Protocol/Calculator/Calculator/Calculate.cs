namespace Calculator
{
    using System;

    public class Calculate
    {
        public static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>Calculator</title>\r\n</head>\r\n<body>\r\n<form action=\"Calculator.exe\" method=\"post\">\r\n    <input type=\"text\" name=\"numberOne\">\r\n    <input type=\"text\" name=\"sign\">\r\n    <input type=\"text\" name=\"numberTwo\">\r\n    <input type=\"submit\" value=\"Calculate\">\r\n</form>\r\n</body>\r\n</html>");

            var inputData = Console.ReadLine().Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
            decimal numberOne = decimal.Parse(inputData[1]);
            string sign = inputData[3];
            decimal numberTwo = decimal.Parse(inputData[5]);

            switch (sign)
            {
                case "%2B":
                    Console.WriteLine("Result: " + (numberOne + numberTwo));
                    break;
                case "-":
                    Console.WriteLine("Result: " + (numberOne - numberTwo));
                    break;
                case "*": 
                    Console.WriteLine("Result: " + (numberOne * numberTwo));
                    break;
                case "%2F": 
                    Console.WriteLine("Result: " + (numberOne / numberTwo));
                    break;
                default:
                    Console.WriteLine("Invalid sign!");
                    break;
            }
        }
    }
}
