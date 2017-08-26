namespace Razor2
{
    public class AboutPage : Page
    {
        public AboutPage(string htmlPath)
            : base(htmlPath)
        {
            this.AddStyleByPath("bootstrap/css/bootstrap.min.css");
        }

        public AboutPage()
             : this("../../content/about.html")
        {
        }
    }
}
