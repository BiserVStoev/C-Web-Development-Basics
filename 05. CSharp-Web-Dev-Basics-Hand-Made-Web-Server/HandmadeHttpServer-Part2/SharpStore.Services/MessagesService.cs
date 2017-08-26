namespace SharpStore.Services
{
    using System.Collections.Generic;
    using Data;
    using Data.Models;

    public class MessagesService
    {
        private SharpStoreContext context;

        public MessagesService()
        {
            this.context = Data.Context;
        }

        public void AddMessageFromPostVars(IDictionary<string, string> vars)
        {
            Message message = new Message()
            {
                Sender = vars["email"],
                MessageText = vars["message"],
                Subject = vars["subject"]
            };

            this.context.Messages.Add(message);
            this.context.SaveChanges();
        }
    }
}
