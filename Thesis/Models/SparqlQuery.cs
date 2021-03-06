﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Thesis.Logic;
using static Thesis.Logic.WebUtils;

namespace Thesis.Models
{
    public class SparqlQuery
    {
        #region Private members 

        #endregion

        #region Properties 

        public string queryBody { get; private set; }

        #endregion

        #region Constructors

        public SparqlQuery(string resource, string queryType)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(WebUtils.DEFAULT_PREFIXES);
            queryBuilder.Append(getSparqlQuery(queryType));
            queryBody = RefactorQuery(queryBuilder.ToString(),resource);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Refactor the query and resource text so it would have valid input
        /// </summary>
        /// <param name="query">The query text</param>
        /// <param name="resource">The querried resoruce</param>
        /// <returns>A formatted query string</returns>
        private static string RefactorQuery(string query, string resource)
        {
            resource = resource.Replace(" ", "_");
            query = query.Replace("res_name", resource);
            return query;
        }

        /// <summary>
        /// Returns the query string 
        /// </summary>
        /// <param name="queryType"></param>
        /// <returns></returns>
        private static string getSparqlQuery(string queryType)
        {
            switch (queryType)
            {
                case QueryType.REGION:
                    return WebUtils.QUERY_REGION;
                case QueryType.ORGANISATIONS:
                    return WebUtils.QUERY_ORGANISATIONS;
                case QueryType.ORGANISATIONS_SINGLE:
                    return WebUtils.QUERY_SINGLE_ORGANISATION_2;
                case QueryType.PERSONS:
                    return WebUtils.QUERY_PERSONS;
                case QueryType.PERSONS_SINGLE:
                    return WebUtils.QUERY_PERSONS_SINGLE;
                case QueryType.LOCATIONS:
                    return WebUtils.QUERY_LOCATIONS;
                case QueryType.LOCATIONS_SINGLE:
                    return WebUtils.QUERY_LOCATIONS_SINGLE;
                case QueryType.EVENTS:
                    return WebUtils.QUERY_EVENTS;
                case QueryType.EVENTS_SINGLE:
                    return WebUtils.QUERY_EVENTS_SINGLE;
                default:
                    return string.Empty;
            }
        }

        #endregion
    }

}