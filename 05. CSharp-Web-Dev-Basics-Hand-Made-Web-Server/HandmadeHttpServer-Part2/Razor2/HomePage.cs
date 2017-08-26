namespace Razor2
{
    public class HomePage : Page
    {
        public HomePage(string htmlPath) : base(htmlPath)
        {
            this.AddStyleByPath("bootstrap/css/bootstrap.min.css");
            this.AddStyleByPath("../../content/css/carousel.css");
        }

        public HomePage() : this("../../content/home.html")
        {
        }
    }
}
