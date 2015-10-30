using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Thesis.Controllers
{
    public class HomeController : Controller
    {
        private const string Url = "http://dbpedia.org/sparql";
        private string urlParam = "?query=";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            ViewBag.Message = "The map";

            return View();
        }

        public string Test(string temp)
        {
            string result = "This is a test " + temp;
            return result;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        //[Route("Home/GetQueryResult/{region}")]
        public string GetQueryResult(string region)
        {
            //var client = new RestClient(endPoint);
            //var json = client.MakeRequest();
            region = region.Replace(" ", "_");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);  
            string query = @"PREFIX res:<http://dbpedia.org/resource/> 
                            PREFIX cc:<http://www.w3.org/2000/01/rdf-schema#>
                            PREFIX ont:<http://dbpedia.org/ontology/>

                            SELECT ?name ?description ?imgLink
                            WHERE 
                            { 
                               res:xRegion cc:label       ?name ;
                                           ont:abstract   ?description ; 
                                           ont:thumbnail  ?imgLink .";
            string filter = "  FILTER (langMatches(lang(?name),\"en\") && langMatches(lang(?description),\"en\")) .}";
            query = query.Replace("xRegion", region);
            query = String.Concat(query, filter);

            query = HttpUtility.UrlEncode(query);
            query = String.Concat(urlParam, query);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(query).Result;  // Blocking call!

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            else
            {
                return "No results found";
            }
        }
    }
}