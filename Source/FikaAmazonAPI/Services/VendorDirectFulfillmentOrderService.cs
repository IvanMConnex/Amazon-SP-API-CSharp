﻿using FikaAmazonAPI.AmazonSpApiSDK.Models.VendorDirectFulfillmentOrders;
using FikaAmazonAPI.Parameter.VendorDirectFulfillmentOrders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FikaAmazonAPI.Services
{
    public class VendorDirectFulfillmentOrderService : RequestService
    {
        public VendorDirectFulfillmentOrderService(AmazonCredential amazonCredential) : base(amazonCredential)
        {

        }


        public List<Order> GetOrders(ParameterVendorDirectFulfillmentGetOrders serachOrderList)
        {
            var orderList = new List<Order>();

            var queryParameters = serachOrderList.getParameters();
            CreateAuthorizedRequest(VendorDirectFulfillmentOrdersApiUrls.GetOrders, RestSharp.Method.GET, parameter: queryParameters);
            var response = ExecuteRequest<GetOrdersResponse>();
            var nextToken = response.Payload?.Pagination?.NextToken;
            orderList.AddRange(response.Payload.Orders);
            while (!string.IsNullOrEmpty(nextToken))
            {
                serachOrderList.NextToken= nextToken;
                var orderPayload = GetOrders(serachOrderList);
                orderList.AddRange(orderPayload);
            }
            return orderList;
        }
        public Order GetOrder(string PurchaseOrderNumber)
        {
            CreateAuthorizedRequest(VendorDirectFulfillmentOrdersApiUrls.GetOrder(PurchaseOrderNumber), RestSharp.Method.GET);
            var response = ExecuteRequest<GetOrderResponse>();
            return response.Payload;
        }
        public TransactionId SubmitAcknowledgement(SubmitAcknowledgementRequest submitAcknowledgementRequest)
        {
            CreateAuthorizedRequest(VendorDirectFulfillmentOrdersApiUrls.SubmitAcknowledgement, RestSharp.Method.POST, postJsonObj: submitAcknowledgementRequest);
            var response = ExecuteRequest<SubmitAcknowledgementResponse>();
            return response.Payload;
        }

    }
}
