namespace SimpleHttpServer.Models
{
    using System.Collections;
    using System.Collections.Generic;

    public class CookieCollection : IEnumerable<Cookie>
    {
        public CookieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }

        public Cookie this[string cookieName]
        {
            get
            {
                return this.Cookies[cookieName];
            }

            set
            {
                this.Add(value);
            }
        }

        public IDictionary<string, Cookie> Cookies { get; private set; }

        public int Count
        {
            get
            {
                return this.Cookies.Count;
            }
        }


        public void Add(Cookie cookie)
        {
            if (!this.Cookies.ContainsKey(cookie.Name))
            {
                this.Cookies.Add(cookie.Name, new Cookie());
            }

            this.Cookies[cookie.Name] = cookie;
        }

        public bool ContainsKey(string key)
        {
            return this.Cookies.ContainsKey(key);
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void RemoveCookie(string cookieName)
        {
            if (this.Cookies.ContainsKey(cookieName))
            {
                this.Cookies.Remove(cookieName);
            }
        }

        IEnumerator<Cookie> IEnumerable<Cookie>.GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }

        public override string ToString()
        {
            var cookie = string.Join("; ", this.Cookies.Values);

            return cookie;
        }
    }
}
