namespace Wizmail.Controllers
{
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;
    using SimpleMVC.Interfaces;
    using Wizmail.BindingModels;
    using Wizmail.Services;
    using Wizmail.Utilities;

    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }

        [HttpGet]
        public IActionResult Register(HttpSession session, HttpResponse response)
        {
            if (AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/mail/inbox");

                return null;
            }

            return this.View();
        }

        [HttpPost]
        public void Register(HttpSession session, HttpResponse response, RegisterUserBm bind)
        {
            if (AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");

                return;
            }

            if (!this.service.IsBindModelValid(bind))
            {
                this.Redirect(response, "/users/register");

                return;
            }

            this.service.RegisterUser(bind);

            this.Redirect(response, "/users/login");
        }

        [HttpGet]
        public IActionResult Login(HttpResponse response, HttpSession session)
        {
            if (AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/mail/inbox");

                return null;
            }

            return this.View();
        }

        [HttpPost]
        public void Login(HttpSession session, HttpResponse response, LoginUserBm bind)
        {
            if (AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/mail/inbox");

                return;
            }

            if (this.service.IsLoginSuccessful(bind, session.Id))
            {
                this.Redirect(response, "/mail/inbox");

                return;
            }

            this.Redirect(response, "/users/login");
        }

        [HttpGet]
        public void Logout(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsUserAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/login");

                return;
            }

            AuthenticationManager.Logout(response, session);

            this.Redirect(response, "/users/login");
        }
    }
}
