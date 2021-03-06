﻿namespace Wizmail.Views.Mail
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SimpleMVC.Interfaces.Generic;
    using Wizmail.Utilities;
    using Wizmail.ViewModels;

    public class Drafts : IRenderable<IEnumerable<EmailVm>>
    {
        public string Render()
        {
            string header = File.ReadAllText(Constants.ContentPath + Constants.HeaderHtml);
            string navigation = File.ReadAllText(Constants.ContentPath + Constants.NavigationLoggedHtml);
            string main = File.ReadAllText(Constants.ContentPath + Constants.EmailDraft);

            StringBuilder mails = new StringBuilder();
            foreach (var emailVm in this.Model)
            {
                mails.AppendLine("<tr>");
                mails.AppendLine("<div class=\"row\">");
                mails.AppendLine("<div class=\"col-sm-3\">");
                mails.AppendLine("<td>");
                mails.AppendLine($"<a href=\"/mail/recieved?id={emailVm.Id}&category=other\">{emailVm.Subject}</a>");
                mails.AppendLine("</td>");
                mails.AppendLine("</div>");
                mails.AppendLine("<div class=\"col-sm-6\">");
                mails.AppendLine("<td>");
                mails.AppendLine(
                    $"<a href=\"/mail/recieved?id={emailVm.Id}&category=sent\">{string.Join("", emailVm.Message.Take(50).ToList())}</a>");
                mails.AppendLine($"</td>");
                mails.AppendLine($"</div>");
                mails.AppendLine($"<div class=\"col-sm-1\">");
                mails.AppendLine($"<td>");
                var attached = string.Empty;
                if (emailVm.Attachment == null)
                {
                    attached = "N";
                }
                else
                {
                    attached = "Y";
                }
                mails.AppendLine($"<a href=\"recieved?id={emailVm.Id}&category=sent\">{attached}</a>");
                mails.AppendLine($"</td>");
                mails.AppendLine($" </div>");
                mails.AppendLine($"<div class=\"col-sm-1\">");
                mails.AppendLine($"<td>");
                mails.AppendLine(
                    $"<a href=\"/mail/recieved?id={emailVm.Id}&category=sent\">{emailVm.SentDate.ToString("yy-MMM-dd")}</a>");
                mails.AppendLine("</td>");
                mails.AppendLine("<td>");
                mails.AppendLine("<form action=\"senddraft\" method=post>\r\n\t");
                mails.AppendLine($"<input name=\"EmailId\" type=\"hidden\" class=\"form-control\" value=\"{emailVm.Id}\">\r\n\t");
                mails.AppendLine("<input type=\"submit\" value=\"Send\" class=\"btn btn-success btn-md\">\r\n</form>");
                mails.AppendLine("</td>");
                mails.AppendLine("</div>");
                mails.AppendLine("<div class=\"col-sm-1\">");
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
