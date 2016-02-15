using System.Collections.Generic;

namespace Thesis.Logic
{
    public static class WebUtils
    {
        public const string DBPEDIA_ENDPOINT = "http://dbpedia.org/sparql";
        public const string SECONDARY_ENDPOINT = "http://live.dbpedia.org/sparql";
        public const string THIRD_ENDPOINT = "http://dbpedia-live.openlinksw.com/sparql";
        public const string FOURTH_ENDPOINT = "http://atted.jp/sparql";
        public const string FITH_ENDPOINT = "http://lod.openlinksw.com/sparql";
        public const string URL_PARAM = "?default-graph-uri=&query=";
        public const string DEFAULT_PREFIXES = "PREFIX res:<http://dbpedia.org/resource/> \r\n"
                                               + "PREFIX cc:<http://www.w3.org/2000/01/rdf-schema#>\r\n"
                                               + "PREFIX ont:<http://dbpedia.org/ontology/>\r\n"
                                               + "PREFIX foaf: <http://xmlns.com/foaf/0.1/>\r\n"
                                               + "PREFIX umbel-rc: <http://umbel.org/reference-concept/?uri=Organization>\r\n"
                                               + "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\r\n"
                                               + "PREFIX owl: <http://www.w3.org/2002/07/owl#>\r\n"
                                               + "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>\r\n"
                                               + "PREFIX dbp: <http://dbpedia.org/property/>\r\n";
        public const string DEFAULT_FILTER = "FILTER(langMatches(lang(?res_name),\"en\")) .}";

        public const string QUERY_ORGANISATIONS = "SELECT DISTINCT ?res_name ?resource \r\n"
                                                  + "WHERE \r\n"
                                                  + "{ {  ?resource ont:type res:Public_company  ; \r\n"
                                                  + "             (rdfs:label |cc:label) ?res_name ; \r\n"
                                                  + "             (ont:location | ont:locationCountry) res:res_name .  } \r\n"
                                                  + "UNION \r\n"
                                                  + "  {  ?resource (ont:type | rdf:type) ont:Company  ; \r\n"
                                                  + "              (rdfs:label | cc:label) ?res_name ; \r\n"
                                                  + "              dbp:location res:res_name . } \r\n"
                                                  + "UNION \r\n"
                                                  + "    { ?resource ont:type res:Public_company;\r\n"
                                                  + "                (cc:label | rdfs:label) ?res_name ;\r\n"
                                                  + "                ont:locationCity ?city . \r\n"
                                                  + "        ?city rdf:type ont:City ; \r\n"
                                                  + "            (cc:label | rdfs:label) ?label ; \r\n"
                                                  + "            ont:country res:res_name . } \r\n"
                                                  + "UNION \r\n"
                                                  + "    {?resource (cc:label | rdfs:label) ?res_name ; \r\n"
                                                  + "        dbp:headquarters res:res_name . } \r\n"
                                                  + "FILTER(langMatches(lang(?res_name),\"en\")) .} ORDER BY ?resource";
        public const string QUERY_REGION = "SELECT ?name ?description ?imgLink \r\n"
                                            + "WHERE \r\n"
                                            + "{ res:res_name cc:label       ?name ; \r\n"
                                            + "               ont:abstract   ?description ; \r\n"
                                            + "               ont:thumbnail  ?imgLink . \r\n"
                                            + "FILTER(langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).}"
                                            + "LIMIT 1";

        public const string QUERY_SINGLE_ORGANISATION = "SELECT ?name ?description (group_concat(distinct ?foundedBy; separator = \",\") as ?founders) "
                                                       + "(group_concat(distinct ?products; separator = \",\") as ?products) ?foundingDate  ?img \r\n"
                                                       + "WHERE {  <res_name> ( dbp:name | cc:label ) ?name ; ont:abstract  ?description. "
                                                       + "OPTIONAL { <res_name> (dbp:founder | ont:foundedBy) ?foundedBy; ont:product ?products; "
                                                       + "(ont:foundingYear | ont:foundingDate) ?foundingDate; ont:thumbnail ?img. } "
                                                       + "FILTER(langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).} LIMIT 1";
        public const string QUERY_SINGLE_ORGANISATION_2 = "SELECT ?name ?description  ?website ?founding_Date ?wikipedia_article  ?img \r\n"
                                                        + "WHERE {  <res_name> ( dbp:name | cc:label ) ?name ; ont:abstract  ?description. OPTIONAL { <res_name> foaf:isPrimaryTopicOf ?wikipedia_article. }"
                                                        + "OPTIONAL { <res_name> (dbp:homepage | foaf:homepage) ?website. } OPTIONAL { <res_name> ont:thumbnail ?img.  } "
                                                        + "OPTIONAL { <res_name> ont:foundingDate ?founding_Date. } FILTER(langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).} LIMIT 1";
        public const string QUERY_PERSONS = "SELECT DISTINCT ?name ?resource "
                                            + "WHERE { {?resource a <http://dbpedia.org/ontology/Person>; (dbp:name | rdfs:label) ?name; <http://dbpedia.org/ontology/birthPlace> ?place. ?place <http://dbpedia.org/ontology/country> ?birthCountry. ?birthCountry a <http://dbpedia.org/ontology/Country>. "
                                            + "FILTER ((?birthCountry = res:res_name) && langMatches(lang(?name ),\"en\") ).} UNION"
              + "{ ?resource a <http://dbpedia.org/ontology/Person>;"
              + " (dbp:name | rdfs:label) ?name;  <http://dbpedia.org/ontology/birthPlace> ?birthCountry. ?birthCountry a <http://dbpedia.org/ontology/Country>."
              + " FILTER ((?birthCountry = res:res_name) && langMatches(lang(?name ),\"en\")).}} LIMIT 500";
        public const string QUERY_PERSONS_2 = "SELECT DISTINCT ?name ?img ?resource ?description  ?wikipedia_article "
                                            + "WHERE { {?resource a <http://dbpedia.org/ontology/Person>; (dbp:name | rdfs:label) ?name; foaf:isPrimaryTopicOf ?wikipedia_article; ont:abstract ?description; ont:thumbnail ?img;  <http://dbpedia.org/ontology/birthPlace> ?place. ?place <http://dbpedia.org/ontology/country> ?birthCountry. ?birthCountry a <http://dbpedia.org/ontology/Country>. "
                                            + "FILTER ((?birthCountry = res:res_name) && langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).} UNION"
              + "{ ?resource a <http://dbpedia.org/ontology/Person>;"
              + " (dbp:name | rdfs:label) ?name; foaf:isPrimaryTopicOf ?wikipedia_article; ont:abstract ?description; ont:thumbnail ?img; dbp:occupation ?occupation; <http://dbpedia.org/ontology/birthPlace> ?birthCountry. ?birthCountry a <http://dbpedia.org/ontology/Country>."
              + " FILTER ((?birthCountry = res:res_name) && langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\")).}} ORDER BY ?resource LIMIT 250";
        public const string QUERY_PERSONS_SINGLE = "SELECT DISTINCT ?name  ?img  ?occupation ?description  ?wikipedia_article "
                                            + "WHERE { { <res_name> (dbp:name | rdfs:label) ?name; ont:abstract ?description.  "
                                            + "OPTIONAL {<res_name> foaf:isPrimaryTopicOf ?wikipedia_article. }"
                                            + "OPTIONAL { <res_name>  ont:thumbnail ?img. } "
                                            + "OPTIONAL { <res_name>  dbp:occupation ?occupation. }"
                                            + "FILTER(langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).}} LIMIT 1";

        public const string QUERY_LOCATIONS = "PREFIX geo: <http://www.w3.org/2003/01/geo/wgs84_pos#> PREFIX dbo: <http://dbpedia.org/ontology/> PREFIX res: <http://dbpedia.org/resource/> SELECT DISTINCT * WHERE  {  { ?resource a dbo:Place. ?resource rdfs:label ?name. ?resource dbo:country res:res_name . } UNION { ?resource a dbo:Location. ?resource rdfs:label ?name. ?resource dbo:country res:res_name . } FILTER(langMatches(lang(?name),\"en\")).  } ORDER BY ?name LIMIT 1000";
        public const string QUERY_LOCATIONS_SINGLE = "SELECT DISTINCT ?name ?description ?wikipedia_article ?population_total WHERE  {  {  <res_name> rdfs:label ?name; ont:abstract ?description; foaf:isPrimaryTopicOf ?wikipedia_article. } OPTIONAL { <res_name>  dbo:populationTotal ?population_total. } OPTIONAL { <res_name>  ont:thumbnail ?img. } FILTER(langMatches(lang(?name ),\"en\") && langMatches(lang(?description),\"en\") ).} LIMIT 1";

        public struct QueryType
        {
            public const string REGION = "region";
            public const string ORGANISATIONS = "organisations";
            public const string ORGANISATIONS_SINGLE = "organisations_single";
            public const string PERSONS = "persons";
            public const string PERSONS_SINGLE = "persons_single";
            public const string LOCATIONS = "locations";
            public const string LOCATIONS_SINGLE = "locations_single";
        }

    }
}
