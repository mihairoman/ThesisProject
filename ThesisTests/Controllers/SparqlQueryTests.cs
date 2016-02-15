using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thesis.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Models;
using Thesis.Logic;
using static Thesis.Logic.WebUtils;

namespace Thesis.Controllers.Tests
{
    [TestClass()]
    public class SparqlQueryTests
    {
        [TestMethod()]
        public void GetQueryResultServerResult()
        {
            SparqlQuery sparqlQuery = new SparqlQuery("United_States", QueryType.REGION);
            string expected = WebUtils.DEFAULT_PREFIXES + WebUtils.QUERY_REGION + WebUtils.DEFAULT_FILTER;
        }
    }
}