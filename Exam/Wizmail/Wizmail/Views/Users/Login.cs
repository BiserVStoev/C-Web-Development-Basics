namespace Wizmail.Views.Users
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using SimpleMVC.Interfaces;
    using Wizmail.Models;
    using Wizmail.Utilities;
    using static Wizmail.Constants;

    public class Login : IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText(ContentPath + HeaderHtml);
            string navigation = File.ReadAllText(ContentPath + NavigationNotLoggedHtml);
            string login = File.ReadAllText(ContentPath + LoginHtml);
            string footer = File.ReadAllText(ContentPath + FooterHtml);

            StringBuilder finalHtml = new StringBuilder();
            finalHtml.Append(header);
            finalHtml.Append(navigation);
            if (ErrorBag.Errors.Count > 0)
            {
                StringBuilder errors = new StringBuilder();

                foreach (var error in ErrorBag.Errors)
                {
                    errors.AppendLine($"<div class=\"alert alert-danger\">\r\n<p>{error.Message}</p>\r\n</div>");
                }

                finalHtml.AppendLine(errors.ToString());
                ErrorBag.Errors = new List<Error>();
            }
            finalHtml.Append(login);
            finalHtml.Append(footer);

            return finalHtml.ToString();
        }
    }
}
