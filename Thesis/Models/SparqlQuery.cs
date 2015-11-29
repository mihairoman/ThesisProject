using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Thesis.Models
{
    public class SparqlQuery
    {
        #region Private members 

        private StringBuilder prefixes;

        private StringBuilder body;

        private StringBuilder filters;

        #endregion

        #region Properties  

        public string Prefixes
        {
            get
            {
                return prefixes.ToString();
            }

            set
            {
                prefixes.AppendLine(value);
            }
        }

        public string Body
        {
            get
            {
                return body.ToString();
            }

            set
            {
                body.Clear();
                body.Append(value);
            }
        }

        public string Filters
        {
            get
            {
                return filters.ToString();
            }

            set
            {
                filters.AppendLine(value);
            }
        }

        #endregion

        #region Constructors

        public SparqlQuery()
        {
        }

        public SparqlQuery(string prefix, string body, string filter)
        {
            Prefixes = prefix;
            Body = body;
            Filters = filter;
        }

        #endregion

        #region Methods

        public string GetSparqlQuery()
        {
            StringBuilder query = new StringBuilder();
            query.Append(Prefixes);
            query.Append(Body);
            query.Append(Filters);
            query.Append("}");
            return query.ToString();
        }

        #endregion
    }
}