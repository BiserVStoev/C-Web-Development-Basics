namespace Razor2
{
    public class ContactsPage : Page
    {
        public ContactsPage(string htmlPath)
            : base(htmlPath)
        {
            this.AddStyleByPath("bootstrap/css/bootstrap.min.css");
        }

        public ContactsPage() : this("../../content/contacts.html") { }
    }
}
