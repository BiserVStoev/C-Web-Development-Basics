namespace Wizmail.Models
{
    using System;
    using System.Collections.Generic;

    public class Email
    {
        public Email()
        {
            this.Recipients = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Attachment { get; set; }

        public DateTime SentDate { get; set; }

        public virtual User Sender { get; set; }

        public virtual ICollection<User> Recipients { get; set; }

        public Flag Flag { get; set; }
    }
}
