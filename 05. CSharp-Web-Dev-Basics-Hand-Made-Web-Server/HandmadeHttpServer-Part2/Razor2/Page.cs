namespace Razor2
{
    using System.IO;
    using System.Text;
    using SimpleHttpServer.Models;

    public class Page
    {
        private StringBuilder htmlContent;

        public Page(string htmlPath)
        {
            this.htmlContent = new StringBuilder(File.ReadAllText(htmlPath));
        }

        public HttpRequest request { get; set; }

        public void AddStyleByPath(string cssPath)
        {
            var headClosingIndex = this.htmlContent.ToString().IndexOf("</head>");
            this.htmlContent.Insert(headClosingIndex, $"<link href=\"{cssPath}\" rel=\"stylesheet\">");
        }

        public override string ToString()
        {
            if (this.request != null && this.request.Header.Cookies.Contains("theme"))
            {
                this.AddStyleByPath(string.Format("../../content/css/{0}.css", this.request.Header.Cookies["theme"].Value));
            }

            return this.htmlContent.ToString();
        }
    }
}
