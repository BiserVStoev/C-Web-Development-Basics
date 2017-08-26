namespace Wizmail.Views.Mail
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using SimpleMVC.Interfaces;
    using Wizmail.Models;
    using Wizmail.Utilities;

    public class New : IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText(Constants.ContentPath + Constants.HeaderHtml);
            string navigation = File.ReadAllText(Constants.ContentPath + Constants.NavigationLoggedHtml);
            string form = File.ReadAllText(Constants.ContentPath + Constants.NewEmail);

            string footer = File.ReadAllText(Constants.ContentPath + Constants.FooterHtml);

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

            finalHtml.Append(form);
            finalHtml.Append(footer);

            return finalHtml.ToString();
        }
    }
}
