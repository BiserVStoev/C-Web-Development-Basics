namespace Wizmail.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public User()
        {
            this.RecievedMessages = new HashSet<Email>();
            this.SentMessages = new HashSet<Email>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Email> SentMessages { get; set; }

        [InverseProperty("Recipients")]
        public virtual ICollection<Email> RecievedMessages { get; set; }
    }
}
