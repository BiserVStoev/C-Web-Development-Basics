namespace Wizmail.Views.Mail
{
    using System.IO;
    using System.Text;
    using SimpleMVC.Interfaces.Generic;
    using Wizmail.ViewModels;
    public class Recieved : IRenderable<DetailedMailVm>
    {
        public string Render()
        {

            string header = File.ReadAllText(Constants.ContentPath + Constants.HeaderHtml);
            string navigation = File.ReadAllText(Constants.ContentPath + Constants.NavigationLoggedHtml);
            string main = File.ReadAllText(Constants.ContentPath + Constants.EmailDetails);
            var attachment = $"<a href=\"{this.Model.Attachment}\" name=\"attachment\" type=\"text\" class=\"form-control\" readonly></a>";
            main = string.Format(main, this.Model.Sender, this.Model.Recipients, this.Model.Sender, this.Model.Message,
                attachment);
            string footer = File.ReadAllText(Constants.ContentPath + Constants.FooterHtml);

            StringBuilder finalHtml = new StringBuilder();
            finalHtml.Append(header);
            finalHtml.Append(navigation);
            finalHtml.Append(main);
            finalHtml.Append(footer);

            return finalHtml.ToString();

        }

        public DetailedMailVm Model { get; set; }
    }
}
