using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpStore.Data;
using SharpStore.Data.Models;
using SimpleHttpServer;

namespace SharpStore.Services
{
    public class KnivesService
    {
        private SharpStoreContext context;

        public KnivesService()
        {
            this.context = Data.Data.Context;
        }

        public IList<Knife> GetAllKnivesFromUrl(string url)
        {
            int indexOfQuestion = url.IndexOf("?");
            if (indexOfQuestion == -1)
            {
                return this.context.Knives.ToList();
            }

            var variables = QueryStringParser.Parse(url.Substring(indexOfQuestion + 1));
            var knifeName = variables["knife-name"];
            var knives = this.context.Knives.Where(k => k.Name.Contains(knifeName)).ToList();

            return knives;
        }
    }
}
