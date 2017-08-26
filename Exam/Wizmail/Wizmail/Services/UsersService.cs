namespace Wizmail.Services
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using AutoMapper;
    using Wizmail.BindingModels;
    using Wizmail.Models;
    using Wizmail.Utilities;
    using static Data.Data;

    public class UsersService
    {
        public void RegisterUser(RegisterUserBm bind)
        {
            User user = Mapper.Instance.Map<RegisterUserBm, User>(bind);

            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public bool IsBindModelValid(RegisterUserBm bind)
        {
            bool isValid = true;

            if (!this.IsUsernameValid(bind))
            {
                isValid = false;
            }

            if (string.IsNullOrEmpty(bind.Password))
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Password is required"
                });
            }

            bind.Password = bind.Password.Trim();
            if (bind.Password.Length < 8 
                || !bind.Password.Any(p => char.IsUpper(p)) 
                || !bind.Password.Any(p => char.IsLower(p)) 
                || !bind.Password.Any(p => char.IsDigit(p)))
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "The password must contain an uppercase letter, a lower case letter and a digit"
                });
            }


            if (string.IsNullOrEmpty(bind.ConfirmPassword) || bind.ConfirmPassword != bind.Password)
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Passwords must match"
                });
            }

            if (string.IsNullOrEmpty(bind.FirstName) 
                || bind.FirstName.Length < 5 
                || bind.FirstName.Length > 30)
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "First name is required and must be between 5 and 30 symbols"
                });
            }

            if (string.IsNullOrEmpty(bind.LastName) 
                || bind.LastName.Length < 5 
                || bind.LastName.Length > 30)
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Last name is required and and must be between 5 and 30 symbols"
                });
            }

            return isValid;
        }

        private bool IsUsernameValid(RegisterUserBm bind)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(bind.Username))
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Username is required"
                });
            }

            string pattern = @"^[a-zA-Z0-9\.]+$";
            Regex regex = new Regex(pattern);

            bool isMatch = regex.IsMatch(bind.Username);
            if (!isMatch)
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Username is should contain letters, digits or ."
                });
            }

            if (bind.Username.Length < 3 || bind.Username.Length > 20)
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Username length must be between 3 and 20 symbols"
                });
            }

            if (Context.Users.Any(u => u.Username == bind.Username))
            {
                isValid = false;
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "Username already taken"
                });
            }

            return isValid;
        }

        public bool IsLoginSuccessful(LoginUserBm bind, string sessionId)
        {
            User wantedUser = Context.Users.FirstOrDefault(user => user.Username == bind.Username && user.Password == bind.Password);
            if (wantedUser == null)
            {
                ErrorBag.Errors.Add(new Error()
                {
                    Message = "<strong>Error!</strong> Invalid Credentials"
                });

                return false;

            }

            Login currentLogin = new Login()
            {
                User = wantedUser,
                IsActive = true,
                SessionId = sessionId
            };

            Context.Logins.Add(currentLogin);
            Context.SaveChanges();

            return true;
        }
    }
}
