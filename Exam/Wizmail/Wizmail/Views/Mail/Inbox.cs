namespace Wizmail.Views.Mail
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SimpleMVC.Interfaces.Generic;
    using Wizmail.Utilities;
    using Wizmail.ViewModels;

    public class Inbox : IRenderable<IEnumerable<EmailVm>>
    {
        public string Render()
        {
            string header = File.ReadAllText(Constants.ContentPath + Constants.HeaderHtml);
            string navigation = File.ReadAllText(Constants.ContentPath + Constants.NavigationLoggedHtml);
            string main = File.ReadAllText(Constants.ContentPath + Constants.Email);

            StringBuilder mails = new StringBuilder();
            foreach (var emailVm in this.Model)
            {
                mails.AppendLine("<tr>");
                mails.AppendLine("<div class=\"row\">");
                mails.AppendLine("<div class=\"col-sm-3\">");
                mails.AppendLine("<td>");
                var subject = string.Empty;
                subject = emailVm.IsRead ? emailVm.Subject : $"<strong>{emailVm.Subject}</strong>";
                mails.AppendLine($"<a href=\"/mail/recieved?id={emailVm.Id}&category=inbox\">{subject}</a>");
                mails.AppendLine("</td>");
                mails.AppendLine("</div>");
                mails.AppendLine("<div class=\"col-sm-6\">");
                mails.AppendLine("<td>");
                var message = emailVm.IsRead
                    ? string.Join("", emailVm.Message.Take(50).ToList())
                    : $"<strong>{string.Join("", emailVm.Message.Take(50).ToList())}</strong>";
                mails.AppendLine($"<a href=\"/mail/recieved?id={emailVm.Id}&category=inbox\">{message}</a>");
                mails.AppendLine($"</td>");
                mails.AppendLine($"</div>");
                mails.AppendLine($"<div class=\"col-sm-1\">");
                mails.AppendLine($"<td>");
                var attached = string.Empty;
                if (emailVm.Attachment == null)
                {
                    attached = emailVm.IsRead ? "N" : "<strong>N</strong>";
                }
                else
                {
                    attached = emailVm.IsRead ? "Y" : "<strong>Y</strong>";
                }
                mails.AppendLine($"<a href=\"recieved?id={emailVm.Id}\">{attached}</a>");  
                mails.AppendLine($"</td>");  
                mails.AppendLine($" </div>");  
                mails.AppendLine($"<div class=\"col-sm-1\">");  
                mails.AppendLine($"<td>");  
                mails.AppendLine($"<a href=\"/mail/recieved?id={emailVm.Id}\">{emailVm.SentDate.ToString("yy-MMM-dd")}</a>");
                mails.AppendLine("</td>");
                mails.AppendLine("</div>");
                mails.AppendLine("<div class=\"col-sm-1\">");
                mails.AppendLine("<td>");
                mails.AppendLine($"<a href=\"/mail/SendToTrash?id={emailVm.Id}\" class=\"btn btn-danger\">X</a>");
                mails.AppendLine("</td>");
                mails.AppendLine("</div>");
                mails.AppendLine("</div>");
                mails.AppendLine("</tr>");
            }

            string unread = string.Empty;
            if (MessagesCountHelper.RecievedMessages > 0)
            {
                unread = $"({MessagesCountHelper.RecievedMessages})";
            }
            string draft = string.Empty;
            if (MessagesCountHelper.DraftMessages > 0)
            {
                draft = $"({MessagesCountHelper.DraftMessages})";
            }
            main = string.Format(main, mails, unread, draft);
            string footer = File.ReadAllText(Constants.ContentPath + Constants.FooterHtml);

            StringBuilder finalHtml = new StringBuilder();
            finalHtml.Append(header);
            finalHtml.Append(navigation);
            finalHtml.Append(main);
            finalHtml.Append(footer);

            return finalHtml.ToString();
        }

        public IEnumerable<EmailVm> Model { get; set; }
    }
}
