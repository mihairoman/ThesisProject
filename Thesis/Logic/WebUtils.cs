using System.Collections.Generic;

namespace Thesis.Logic
{
    public static class WebUtils
    {
        public const string DBPEDIA_ENDPOINT = "http://dbpedia.org/sparql";
        public const string SECONDARY_ENDPOINT = "http://lod.openlinksw.com/sparql";
        public const string URL_PARAM = "?query=";
        public const string DEFAULT_PREFIXES = "PREFIX res:<http://dbpedia.org/resource/> \r\n"
                                               + "PREFIX cc:<http://www.w3.org/2000/01/rdf-schema#>\r\n"
                                               + "PREFIX ont:<http://dbpedia.org/ontology/>\r\n"
                                               + "PREFIX foaf: <http://xmlns.com/foaf/>\r\n"
                                               + "PREFIX umbel-rc: <http://umbel.org/reference-concept/?uri=Organization>\r\n"
                                               + "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\r\n"
                                               + "PREFIX owl: <http://www.w3.org/2002/07/owl#>\r\n"
                                               + "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>\r\n"
                                               + "PREFIX dbp: <http://dbpedia.org/property/>\r\n";
        public const string DEFAULT_FILTER = "FILTER(langMatches(lang(?res_name),\"en\")) .}";

        public const string QUERY_ORGANISATIONS = "SELECT DISTINCT ?res_name \r\n"
                                                  + "WHERE \r\n"
                                                  + "{ {  ?company ont:type res:Public_company  ; \r\n"
                                                  + "             (rdfs:label |cc:label) ?res_name ; \r\n"
                                                  + "             (ont:location | ont:locationCountry) res:res_name .  } \r\n"
                                                  + "UNION \r\n"
                                                  + "  {  ?company (ont:type | rdf:type) ont:Company  ; \r\n"
                                                  + "              (rdfs:label | cc:label) ?res_name ; \r\n"
                                                  + "              dbp:location res:res_name . } \r\n"
                                                  + "UNION \r\n"
                                                  + "    { ?company ont:type res:Public_company;\r\n"
                                                  + "                (cc:label | rdfs:label) ?res_name ;\r\n"
                                                  + "                ont:locationCity ?city . \r\n"
                                                  + "        ?city rdf:type ont:City ; \r\n"
                                                  + "            (cc:label | rdfs:label) ?label ; \r\n"
                                                  + "            ont:country res:res_name . } \r\n"
                                                  + "UNION \r\n"
                                                  + "    {?company (cc:label | rdfs:label) ?res_name ; \r\n"
                                                  + "        dbp:headquarters res:res_name . } \r\n"
                                                  + "FILTER(langMatches(lang(?res_name),\"en\")) .}";
        public const string QUERY_REGION = "SELECT ?name ?description ?imgLink \r\n"
                                            + "WHERE \r\n"
                                            + "{ res:res_name cc:label       ?name ; \r\n"
                                            + "               ont:abstract   ?description ; \r\n"
                                            + "               ont:thumbnail  ?imgLink . \r\n"
                                            + "FILTER(langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).}"
                                            + "LIMIT 1";

        public const string QUERY_SINGLE_ORGANISATION = "SELECT ?name ?description (group_concat(distinct ?foundedBy; separator = \",\") as ?founders) "
                                                       + "(group_concat(distinct ?products; separator = \",\") as ?products) ?foundingDate  ?img \r\n"
                                                       + "WHERE {  <http://dbpedia.org/resource/res_name> ( dbp:name | cc:label ) ?name ; ont:abstract  ?description. "
                                                       + "OPTIONAL { res: ont:foundedBy ?foundedBy; ont:product ?products; "
                                                       + "ont:foundingDate ?foundingDate; ont:thumbnail ?img. } "
                                                       + "FILTER(langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).} LIMIT 1";
    }

    public struct QueryType
    {
        public const string REGION = "region";
        public const string ORGANISATIONS = "organisations";
        public const string ORGANISATIONS_SINGLE = "organisations_single";
        public const string PERSONS = "persons";
        public const string PERSONS_SINGLE = "persons_single";
    }

}
