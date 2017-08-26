namespace Razor2
{
    using System.Collections.Generic;
    using System.Text;
    using SharpStore.Data.Models;

    public class ProductsPage : Page
    {
        private IList<Knife> knives;
        public ProductsPage(string htmlPath, IList<Knife> knives) : base(htmlPath)
        {
            this.AddStyleByPath("bootstrap/css/bootstrap.min.css");
            this.knives = knives;
        }

        public ProductsPage(IList<Knife> knives) : this("../../content/products.html", knives)
        {
        }

        public override string ToString()
        {
            StringBuilder knivesStr = new StringBuilder();
            foreach (var knife in this.knives)
            {
                knivesStr.Append(
                    "<div class=\"col-md-4 col-lg-4\">\r\n" +
                    "<img class=\"featurette-image img-responsive\" " +
                    $"src=\"{knife.ImageUrl}\">\r\n" +
                    $"<h2>{knife.Name}</h2>\r\n" +
                    $"<p>${knife.Price}</p>\r\n" +
                    "<button class=\"btn btn-primary\">Buy Now</button>\r\n" +
                    "</div>");
            }

            return string.Format(base.ToString(), knivesStr.ToString());
        }
    }
}
