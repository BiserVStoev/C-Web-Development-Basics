namespace Wizmail.ViewModels
{
    using System;

    public class EmailVm
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Attachment { get; set; }

        public DateTime SentDate { get; set; }

        public bool IsRead { get; set; }
    }
}
