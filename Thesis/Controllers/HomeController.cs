using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Thesis.Logic;
using Thesis.Models;

namespace Thesis.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            ViewBag.Message = "The map";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        //[HttpGet]
        //public string GetQueryResult(string region)
        //{
        //    //var client = new RestClient(endPoint);
        //    //var json = client.MakeRequest();
        //    region = region.Replace(" ", "_");
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(Url);  
        //    string query = @"PREFIX res:<http://dbpedia.org/resource/> 
        //                    PREFIX cc:<http://www.w3.org/2000/01/rdf-schema#>
        //                    PREFIX ont:<http://dbpedia.org/ontology/>

        //                    SELECT ?name ?description ?imgLink
        //                    WHERE 
        //                    { 
        //                       res:xRegion cc:label       ?name ;
        //                                   ont:abstract   ?description ; 
        //                                   ont:thumbnail  ?imgLink .";
        //    string filter = "  FILTER (langMatches(lang(?name),\"en\") && langMatches(lang(?description),\"en\")) .}";
        //    query = query.Replace("xRegion", region);
        //    query = String.Concat(query, filter);

        //    query = HttpUtility.UrlEncode(query);
        //    query = String.Concat(urlParam, query);
        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = client.GetAsync(query).Result;  // Blocking call!

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string result = response.Content.ReadAsStringAsync().Result;
        //        return result;
        //    }
        //    else
        //    {
        //        return "No results found";
        //    }
        //}

    //    [HttpGet]
    //    public string GetOrganizations(string region)
    //    {
    //        region = region.Replace(" ", "_");
    //        HttpClient client = new HttpClient();
    //        client.BaseAddress = new Uri(Url);
    //        string query = @"PREFIX res:<http://dbpedia.org/resource/> 
    //                        PREFIX cc:<http://www.w3.org/2000/01/rdf-schema#>
    //                        PREFIX ont:<http://dbpedia.org/ontology/>
    //                        PREFIX foaf: <http://xmlns.com/foaf/>
    //                        PREFIX umbel-rc: <http://umbel.org/reference-concept/?uri=Organization>
    //                        PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    //                        PREFIX owl: <http://www.w3.org/2002/07/owl#> 
    //                        PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
    //                        PREFIX dbp: <http://dbpedia.org/property/>

    //                        SELECT DISTINCT ?res_name 
    //                        WHERE 
    //                        { 
                             
    //                         {  ?company ont:type res:Public_company  ;
    //                                   (rdfs:label |cc:label) ?res_name ;
    //                                   (ont:location | ont:locationCountry) res:xRegion . 

    //                          }
    //                        UNION 
    //                               {  ?company (ont:type | rdf:type) ont:Company  ;
    //                                           (rdfs:label | cc:label) ?res_name ;
    //                                           dbp:location res:xRegion . 
    //                          }
    //                          UNION
    //                          {
    //                             ?company ont:type res:Public_company;
    //                                      (cc:label | rdfs:label) ?res_name ;
    //                                        ont:locationCity ?city .
    //                              ?city rdf:type ont:City ; 
    //                                    (cc:label | rdfs:label) ?label ; 
    //                                    ont:country res:xRegion .
    //                          }
    //                          UNION 
    //                               {
    //                                ?company (cc:label | rdfs:label) ?res_name ;
    //                                 dbp:headquarters res:xRegion .
    //                               }";
   
    //        string filter =  "FILTER (langMatches(lang(?res_name),\"en\")) .}";
    //        query = query.Replace("xRegion", region);
    //        query = String.Concat(query, filter);

    //        query = HttpUtility.UrlEncode(query);
    //        query = String.Concat(urlParam, query);
    //        // Add an Accept header for JSON format.
    //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //        HttpResponseMessage response = client.GetAsync(query).Result;  // Blocking call!

    //        if (response.IsSuccessStatusCode)
    //        {
    //            string result = response.Content.ReadAsStringAsync().Result;
    //            return result;
    //        }
    //        else
    //        {
    //            return "No results found";
    //        }
    //    }
    }
}