using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thesis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Logic;
using static Thesis.Logic.WebUtils;

namespace Thesis.Models.Tests
{
    [TestClass()]
    public class SparqlQueryTests
    {
        [TestMethod()]
        public void SparqlQueryTestSingleParamConstructor()
        {
            SparqlQuery sparqlQuery = new SparqlQuery("United_States",QueryType.REGION);
            string expected = WebUtils.DEFAULT_PREFIXES + WebUtils.QUERY_REGION;
            Assert.AreEqual(sparqlQuery.queryBody, expected);
        }

     
    }
}