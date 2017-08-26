namespace signin
{
    using PizzaMore.Data;
    using PizzaMore.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SignIn
    {
        public static IDictionary<string, string> RequestParameters;
        public static Header Header = new Header();

        public static void Main()
        {
            if (WebUtil.IsPost())
            {
                LogIn();
            }

            ShowPage();
        }

        private static void LogIn()
        {
            RequestParameters = WebUtil.RetrievePostParameters();
            string email = RequestParameters["email"];
            string password = RequestParameters["password"];
            string hashedPassword = PasswordHasher.Hash(password);
            using (var context = new PizzaMoreContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Email == email);
                if (hashedPassword == user.Password)
                {
                    var session = new Session()
                    {
                        Id = new Random().Next().ToString(),
                        User = user
                    };

                    if (user != null)
                    {
                        Header.AddCookie(new Cookie("sid", session.Id));
                    }
                    context.Sessions.Add(session);
                    context.SaveChanges();
                }
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../../htdocs/pm/signin.html");
        }
    }
}