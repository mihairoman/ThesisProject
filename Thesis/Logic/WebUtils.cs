namespace Thesis.Logic
{
    public static class WebUtils
    {
        public static readonly string DEFAULT_PREFIXES = @"PREFIX res:<http://dbpedia.org/resource/> 
                                                        PREFIX cc:<http://www.w3.org/2000/01/rdf-schema#>
                                                        PREFIX ont:<http://dbpedia.org/ontology/>
                                                        PREFIX foaf: <http://xmlns.com/foaf/>
                                                        PREFIX umbel-rc: <http://umbel.org/reference-concept/?uri=Organization>
                                                        PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                                                        PREFIX owl: <http://www.w3.org/2002/07/owl#> 
                                                        PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
                                                        PREFIX dbp: <http://dbpedia.org/property/>";
        public static readonly string DEFAULT_FILTER = "FILTER(langMatches(lang(?res_name),\"en\")) .";

        public static readonly string QUERY_ORGANISATIONS = @"SELECT DISTINCT ?res_name 
                                                            WHERE 
                                                            { 
                             
                                                                {  ?company ont:type res:Public_company  ;
                                                                        (rdfs:label |cc:label) ?res_name ;
                                                                        (ont:location | ont:locationCountry) res:xRegion . 

                                                                }
                                                            UNION 
                                                                    {  ?company (ont:type | rdf:type) ont:Company  ;
                                                                                (rdfs:label | cc:label) ?res_name ;
                                                                                dbp:location res:xRegion . 
                                                                }
                                                                UNION
                                                                {
                                                                    ?company ont:type res:Public_company;
                                                                            (cc:label | rdfs:label) ?res_name ;
                                                                            ont:locationCity ?city .
                                                                    ?city rdf:type ont:City ; 
                                                                        (cc:label | rdfs:label) ?label ; 
                                                                        ont:country res:xRegion .
                                                                }
                                                                UNION 
                                                                    {
                                                                    ?company (cc:label | rdfs:label) ?res_name ;
                                                                        dbp:headquarters res:xRegion .
                                                                    }";
        public static readonly string  QUERY_REGION = @"SELECT ?name ?description ?imgLink
                                                        WHERE 
                                                        { 
                                                            res:xRegion cc:label       ?name ;
                                                                        ont:abstract   ?description ; 
                                                                        ont:thumbnail  ?imgLink .";
    }
}
