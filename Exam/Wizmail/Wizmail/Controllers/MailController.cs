namespace Wizmail.Controllers
{
    using System.Collections.Generic;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;
    using SimpleMVC.Interfaces;
    using SimpleMVC.Interfaces.Generic;
    using Wizmail.BindingModels;
    using Wizmail.Models;
    using Wizmail.Services;
    using Wizmail.Utilities;
    using Wizmail.ViewModels;

    public class MailController : Controller
    {
        private MailService service;

        public MailController()
        {
            this.service = new MailService();
        }

        [HttpGet]
        public IActionResult<IEnumerable<EmailVm>> Inbox(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return null;
            }

            User currentUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            IEnumerable<EmailVm> vms = this.service.GetUserInbox(currentUser.Id);

            return this.View(vms);
        }

        [HttpGet]
        public IActionResult New(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return null;
            }

            User currentUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            return this.View();
        }

        [HttpPost]
        public IActionResult New(HttpSession session, HttpResponse response, NewMailBm bm)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return null;
            }

            if (!this.service.NewMailBmIsValid(bm))
            {
                this.Redirect(response, "/mail/new");

                return null;
            }

            User currentUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (bm.Action == "Send")
            {
                this.service.AddNewMail(bm, currentUser.Id);
            }
            else if (bm.Action == "Save")
            {
                this.service.PutMailInDraft(bm, currentUser.Id);
            }
            

            this.Redirect(response, "mail/sent");

            return null;
        }

        [HttpGet]
        public IActionResult<DetailedMailVm> Recieved(HttpSession session, HttpResponse response, int id, string category)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return null;
            }

            DetailedMailVm vm = this.service.GetDetailedMailVm(id, category);

            if (vm == null)
            {
                this.Redirect(response, "/mail/inbox");

                return null;
            }

            return this.View(vm);
        }

        [HttpGet]
        public IActionResult<IEnumerable<EmailVm>> Sent(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return null;
            }

            User currentUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            IEnumerable<EmailVm> vms = this.service.GetUserSentMail(currentUser.Id);

            return this.View(vms);
        }

        [HttpGet]
        public void SendToTrash(HttpSession session, HttpResponse response, int id)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return;
            }

            this.service.MoveMailToTrash(id);

            this.Redirect(response, "/mail/inbox");
        }

        [HttpGet]
        public IActionResult<IEnumerable<EmailVm>> Drafts(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return null;
            }

            User currentUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            IEnumerable<EmailVm> vms = this.service.GetUserDraftMail(currentUser.Id);

            return this.View(vms);
        }

        [HttpPost]
        public IActionResult Senddraft(HttpSession session, HttpResponse response, SendDraftBm bm)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return null;
            }

            this.service.SendDraft(bm);

            this.Redirect(response, "mail/drafts");

            return null;
        }
    }
}
