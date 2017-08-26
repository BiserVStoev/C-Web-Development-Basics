namespace Wizmail.Utilities
{
    using System.Collections.Generic;
    using Wizmail.Models;

    public static class ErrorBag
    {
        static ErrorBag()
        {
            Errors = new List<Error>();   
        }

        public static List<Error> Errors { get; set; }
    }
}
