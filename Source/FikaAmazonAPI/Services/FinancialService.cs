﻿using FikaAmazonAPI.AmazonSpApiSDK.Models.Finances;
using FikaAmazonAPI.Parameter.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace FikaAmazonAPI.Services
{
    public class FinancialService : RequestService
    {
        public FinancialService(AmazonCredential amazonCredential) : base(amazonCredential)
        {

        }


        public IList<FinancialEventGroup> ListFinancialEventGroups(ParameterListFinancialEventGroup  parameterListFinancialEventGroup)
        {
            List<FinancialEventGroup> list = new List<FinancialEventGroup>();

            var parameter = parameterListFinancialEventGroup.getParameters();

            CreateAuthorizedRequest(FinanceApiUrls.ListFinancialEventGroups , RestSharp.Method.GET, parameter);
            var response = ExecuteRequest<ListFinancialEventGroupsResponse>();

            list.AddRange(response.Payload.FinancialEventGroupList);
            var nextToken = response.Payload.NextToken;
            
            while (!string.IsNullOrEmpty(nextToken))
            {
                var data = GetFinancialEventGroupListByNextToken(nextToken);
                list.AddRange(data.FinancialEventGroupList);
                nextToken = data.NextToken;
            }

            return list;
        }

        private ListFinancialEventGroupsPayload GetFinancialEventGroupListByNextToken(string nextToken)
        {
            List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
            queryParameters.Add(new KeyValuePair<string, string>("NextToken", nextToken));


            CreateAuthorizedRequest(FinanceApiUrls.ListFinancialEventGroups, RestSharp.Method.GET, queryParameters);
            var response = ExecuteRequest<ListFinancialEventGroupsResponse>();
            return response.Payload;
        }


        private FinancialEvents ListFinancialEventsByGroupId(string eventGroupId)
        {
            CreateAuthorizedRequest(FinanceApiUrls.ListFinancialEventsByGroupId(eventGroupId), RestSharp.Method.GET);
            var response = ExecuteRequest<ListFinancialEventsResponse>();
            return response.Payload.FinancialEvents;
        }

        public FinancialEvents ListFinancialEventsByOrderId(string orderId)
        {
            CreateAuthorizedRequest(FinanceApiUrls.ListFinancialEventsByOrderId(orderId), RestSharp.Method.GET);
            var response = ExecuteRequest<ListFinancialEventsResponse>();
            return response.Payload.FinancialEvents;
        }

        private FinancialEvents ListFinancialEvents()
        {
            CreateAuthorizedRequest(FinanceApiUrls.ListFinancialEvents, RestSharp.Method.GET);
            var response = ExecuteRequest<ListFinancialEventsResponse>();
            return response.Payload.FinancialEvents;
        }

        public IList<FinancialEvents> ListFinancialEvents(ParameterListFinancialEvents parameterListFinancials)
        {
            List<FinancialEvents> list = new List<FinancialEvents>();

            var parameter = parameterListFinancials.getParameters();
            //var parameter = parameterListFinancials.GetParamsDict();

            CreateAuthorizedRequest(FinanceApiUrls.ListFinancialEvents, RestSharp.Method.GET, parameter);
            var response = ExecuteRequest<ListFinancialEventsResponse>();

            list.Add(response.Payload.FinancialEvents);
            var nextToken = response.Payload.NextToken;

            while (!string.IsNullOrEmpty(nextToken))
            {
                var data = GetFinancialEventsByNextToken(nextToken);
                list.Add(data.FinancialEvents);
                nextToken = data.NextToken;
            }

            return list;
        }

        private ListFinancialEventsPayload GetFinancialEventsByNextToken(string nextToken)
        {
            List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
            queryParameters.Add(new KeyValuePair<string, string>("NextToken", nextToken));


            CreateAuthorizedRequest(FinanceApiUrls.ListFinancialEvents, RestSharp.Method.GET, queryParameters);
            var response = ExecuteRequest<ListFinancialEventsResponse>();
            return response.Payload;
        }


    }
}
