using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Thesis.Logic;
using Thesis.Models;
using static Thesis.Logic.WebUtils;

namespace Thesis.Controllers
{
    public class MapController : Controller
    {
        [HttpGet]
        public async Task<string> GetQueryResult(string resource, string queryType)
        {
            SparqlQuery sparqlQuery = new SparqlQuery(resource, queryType);
            string query = sparqlQuery.queryBody;
            using (HttpClient client = new HttpClient())
            {
                query = HttpUtility.UrlEncode(query);
                query = string.Concat(WebUtils.URL_PARAM, query);
                if (!queryType.Equals(QueryType.REGION) && !queryType.Equals(QueryType.ORGANISATIONS) 
                    && !queryType.Equals(QueryType.PERSONS) && !queryType.Equals(QueryType.PERSONS_SINGLE) && 
                    !queryType.Equals(QueryType.LOCATIONS) && !queryType.Equals(QueryType.EVENTS))
                {
                    query = string.Concat(query, "&should-sponge=grab-all-seealso");
                    client.BaseAddress = new Uri(WebUtils.THIRD_ENDPOINT);
                }
                else
                {
                    client.BaseAddress = new Uri(WebUtils.DBPEDIA_ENDPOINT);
                }
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> responseTask = client.GetAsync(query);
                HttpResponseMessage responseMsg = await responseTask;
                if (responseMsg.IsSuccessStatusCode)
                {
                    Task<string> contentTask = responseMsg.Content.ReadAsStringAsync();
                    string result = await contentTask;
                    return result;
                }
                else
                {
                    return "No results found";
                }
            }
        }
    }
}