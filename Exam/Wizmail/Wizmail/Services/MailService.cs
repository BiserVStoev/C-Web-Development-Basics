namespace Wizmail.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AutoMapper;
    using Wizmail.BindingModels;
    using Wizmail.Models;
    using Wizmail.Utilities;
    using Wizmail.ViewModels;
    using static Data.Data;

    public class MailService
    {
        public IEnumerable<EmailVm> GetUserInbox(int currentUserId)
        {
            User currentUser = Context.Users.Find(currentUserId);
            IEnumerable<EmailVm> vms = Mapper.Map<IEnumerable<Email>, IEnumerable<EmailVm>>(currentUser.RecievedMessages
                .Where(m => m.Flag == Flag.Sent || m.Flag == Flag.Read));

            MessagesCountHelper.RecievedMessages =
                currentUser.RecievedMessages.Count(m => m.Flag == Flag.Sent);
            MessagesCountHelper.DraftMessages = currentUser.SentMessages.Count(m => m.Flag == Flag.Draft);


            return vms;
        }

        public bool NewMailBmIsValid(NewMailBm bm)
        {
            var users = bm.Users.Split(';').ToList();
            bool isValid = true;
            if (users.Count == 0 || string.IsNullOrEmpty(string.Join("", users)))
            {
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Recipients are required"
                });

                isValid = false;
            }

            foreach (var user in users)
            {
                string pattern = @"^[a-zA-Z0-9\.]+@wizmail.bg\s*?$";
                Regex regex = new Regex(pattern);

                bool isMatch = regex.IsMatch(user);
                if (!isMatch)
                {
                    ErrorBag.Errors.Add(new Error()
                    {
                        Message = "Recipients should be in format: \"username@wizmail.bg\""
                    });

                    isValid = false;
                    break;
                }
            }

            if (bm.Subject.Length < 3 || bm.Subject.Length > 50)
            {
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Subject must be between 3 and 50 symbols"
                });

                isValid = false;
            }

            if (bm.Message.Length > 300)
            {
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Message must less than 300 symbols"
                });

                isValid = false;
            }

            if (bm.Attachment.Length > 250)
            {
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Attachment must be less than 250 symbols"
                });

                isValid = false;
            }

            return isValid;
        }

        public void AddNewMail(NewMailBm bm, int currentUserId)
        {
            Email mail = Mapper.Map<NewMailBm, Email>(bm);
            mail.SentDate = DateTime.Now;
            User currentuser = Context.Users.Find(currentUserId);
            mail.Flag = Flag.Sent;
            mail.Sender = currentuser;
            if (mail.Attachment != null && mail.Attachment.Length == 0)
            {
                mail.Attachment = null;
            }

            var users = bm.Users.Split(';').Select(u => u.Trim()).ToList();
            foreach (var user in users)
            {
                var username = user.Split('@')[0];
                if (Context.Users.Any(u => u.Username == username))
                {
                    var reciever = Context.Users.FirstOrDefault(u => u.Username == username);
                    mail.Recipients.Add(reciever);
                }
            }

            Context.Emails.Add(mail);
            Context.SaveChanges();
        }

        public DetailedMailVm GetDetailedMailVm(int emailId, string category)
        {
            Email mail = Context.Emails.Find(emailId);

            if (mail == null)
            {
                return null;
            }

            if (category == "inbox")
            {
                mail.Flag = Flag.Read;
                Context.SaveChanges();
            }

            
            DetailedMailVm vm = Mapper.Map<Email, DetailedMailVm>(mail);

            return vm;
        }

        public IEnumerable<EmailVm> GetUserSentMail(int currentUserId)
        {

            User currentUser = Context.Users.Find(currentUserId);
            IEnumerable<EmailVm> vms = Mapper.Map<IEnumerable<Email>, IEnumerable<EmailVm>>(currentUser.SentMessages.Where(m => m.Flag != Flag.Draft));
            MessagesCountHelper.RecievedMessages =
                currentUser.RecievedMessages.Count(m => m.Flag == Flag.Sent);
            MessagesCountHelper.DraftMessages = currentUser.SentMessages.Count(m => m.Flag == Flag.Draft);

            return vms;
        }

        public void MoveMailToTrash(int id)
        {
            var mail = Context.Emails.Find(id);
            mail.Flag = Flag.InTrash;
            Context.SaveChanges();
        }

        public void PutMailInDraft(NewMailBm bm, int currentUserId)
        {
            Email mail = Mapper.Map<NewMailBm, Email>(bm);
            mail.SentDate = DateTime.Now;
            User currentuser = Context.Users.Find(currentUserId);
            mail.Flag = Flag.Draft;
            mail.Sender = currentuser;
            if (mail.Attachment != null && mail.Attachment.Length == 0)
            {
                mail.Attachment = null;
            }

            var users = bm.Users.Split(';').Select(u => u.Trim()).ToList();
            foreach (var user in users)
            {
                var username = user.Split('@')[0];
                if (Context.Users.Any(u => u.Username == username))
                {
                    var reciever = Context.Users.FirstOrDefault(u => u.Username == username);
                    mail.Recipients.Add(reciever);
                }
            }

            Context.Emails.Add(mail);
            Context.SaveChanges();
        }

        public IEnumerable<EmailVm> GetUserDraftMail(int currentUserId)
        {
            User currentUser = Context.Users.Find(currentUserId);
            IEnumerable<EmailVm> vms = Mapper.Map<IEnumerable<Email>, IEnumerable<EmailVm>>(currentUser.SentMessages.Where(m => m.Flag == Flag.Draft));
            MessagesCountHelper.RecievedMessages =
                currentUser.RecievedMessages.Count(m => m.Flag == Flag.Sent);
            MessagesCountHelper.DraftMessages = currentUser.SentMessages.Count(m => m.Flag == Flag.Draft);

            return vms;
        }

        public void SendDraft(SendDraftBm bm)
        {
            var mail = Context.Emails.Find(bm.EmailId);
            mail.Flag = Flag.Sent;
            Context.SaveChanges();
        }
    }
}
