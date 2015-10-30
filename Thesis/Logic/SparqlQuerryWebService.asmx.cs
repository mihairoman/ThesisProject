using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Thesis.Logic
{
    /// <summary>
    /// Summary description for SparqlQuerryWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SparqlQuerryWebService : System.Web.Services.WebService
    {
        private SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org/sparql");
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        private const string Url = "http://dbpedia.org/sparql";
        private const string urlParam = "?query=";

        [WebMethod]
        public string GenerateSparqlQuery(string region)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(urlParam).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Object>>().Result;
                foreach (var d in dataObjects)
                {
                }
            }
            else
            {

            }  



            return string.Empty;
//            SparqlParameterizedString queryString = new SparqlParameterizedString();
//            //queryString.Namespaces.AddNamespace("res", new Uri("http://dbpedia.org/resource/"));
//            queryString.Namespaces.AddNamespace("cc", new Uri("http://www.w3.org/2000/01/rdf-schema#"));
//            queryString.Namespaces.AddNamespace("ont", new Uri("http://dbpedia.org/ontology/"));
//            queryString.CommandText = @"SELECT ?name ?description
//                                        WHERE 
//                                        { 
//                                               @resource cc:label ?name ;
//                                                         ont:abstract ?description .";
//            queryString.CommandText += " FILTER (langMatches(lang(?name),\"en\") && langMatches(lang(?description),\"en\")) .}";
//            queryString.SetUri("resource", new Uri("http://dbpedia.org/resource/"+region));
//            //SparqlQueryParser parser = new SparqlQueryParser();
//            //SparqlQuery query = parser.ParseFromString(queryString);
            
//            //string tmp = queryString.ToString();

//            RemoteQueryProcessor processor = new RemoteQueryProcessor(endpoint);
//            queryString.QueryProcessor = processor;
            
//            //var tttt = queryString.ExecuteQuery();

//            //var tmp = queryString.ToString().Replace("(","%28");
//            //tmp = tmp.Replace(")","%29");

//            var tttt = endpoint.QueryWithResultSet(queryString.ToString());
            

            //ISparqlQueryProcessor processor = new RemoteQueryProcessor(endpoint);
            //var termp = query.Process(processor);
            //var results = processor.ProcessQuery(query);
            //var fff = queryString.ExecuteQuery();
            //var tttt = endpoint.QueryWithResultSet();
            //SparqlResultSet results = ( processor.ProcessQuery(query);
            //Define a remote endpoint
            //Use the DBPedia SPARQL endpoint with the default Graph set to DBPedia
            //SparqlResultSet result = queryString.ExecuteQuery();

            //Define a remote endpoint
            //Use the DBPedia SPARQL endpoint with the default Graph set to DBPedia
            //Make a SELECT query against the Endpoint
            //var results = endpoint.QueryWithResultSet(querry);
            //foreach (var result in results)
            //{
            //    var temp = result;
            //}

            //Make a DESCRIBE query against the Endpoint

            //Graph gr = new Graph();
            //string temp = "http://dbpedia.org/resource/" + region;
            //Uri resource = new Uri(temp);
            //UriLoader.Load(gr,resource);
            //IEnumerable<Triple> triples = gr.GetTriples(new Uri("http://dbpedia.org/ontology/abstract"));
            //string result = "";
            //foreach (Triple trip in triples)
            //{
            //    ILiteralNode te = trip.Object as ILiteralNode;
            //    if (te.Language.Equals("en") || (String.IsNullOrEmpty(te.Language) && String.IsNullOrEmpty(te.Value)))
            //    {
            //        result = te.ToString();
            //        break;
            //    }
            //}
            //return result;
            //Options.HttpDebugging = true;
            //queryString.Namespaces.AddNamespace("ont", new Uri("http://dbpedia.org/ontology/abstract"));
            //queryString.CommandText = "SELECT ?des WHERE { @value ont: ?des FILTER (langMatches(lang(?des),\"en\"))}";
            //queryString.SetUri("value", new Uri("http://dbpedia.org/resource/" + region));
            //SparqlQueryParser parser = new SparqlQueryParser();
            //SparqlQuery query = parser.ParseFromString(queryString);
            //string temp = query.ToString();
            //temp = string.Concat(temp, "&output=json");
            //SparqlResultSet results = endpoint.QueryWithResultSet(query.ToString());

            //return results.ToString();
            //string endQuery = query.ToString();
            //string temp = String.Empty;
            
            //try
            //{
            //    SparqlResultSet results = endpoint.QueryWithResultSet(endQuery);
            //    //if (results.Count > 0)
            //    //{
            //    temp = _javaScriptSerializer.Serialize(results[0].ToString());
            //    //};
            //    return temp;

            //    foreach (SparqlResult r in results)
            //    {
            //        //r.HasBoundValue(r.Value);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return temp;
            //}
             
        }
    }
}
