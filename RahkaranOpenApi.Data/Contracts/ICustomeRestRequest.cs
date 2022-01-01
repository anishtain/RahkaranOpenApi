using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Data.Contracts
{
    public interface ICustomeRestRequest
    {
        Task<IRestResponse<TResponse>> Post<TRequest, TResponse>(string url, TRequest input, params KeyValuePair<string, string>[] headers);

        Task<IRestResponse<TResponse>> Get<TRequest, TResponse>(string url, TRequest input, params KeyValuePair<string, string>[] headers);
    }
}
