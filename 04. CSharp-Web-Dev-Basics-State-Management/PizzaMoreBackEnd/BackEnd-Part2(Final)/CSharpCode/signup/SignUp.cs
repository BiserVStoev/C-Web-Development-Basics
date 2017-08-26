namespace signup
{
    using PizzaMore.Data;
    using PizzaMore.Utilities;
    using System.Collections.Generic;

    public class SignUp
    {
        public static IDictionary<string, string> RequestParameters;
        public static Header Header = new Header();
        public static void Main()
        {
            if (WebUtil.IsPost())
            {
                RegisterUser();
            }

            ShowPage();
        }

        private static void RegisterUser()
        {
            RequestParameters = WebUtil.RetrievePostParameters();
            var email = RequestParameters["email"];
            var password = RequestParameters["password"];
            var user = new User()
            {
                Email = email,
                Password = PasswordHasher.Hash(password)
            };

            using (var context = new PizzaMoreContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../../htdocs/pm/signup.html");
        }
    }
}
