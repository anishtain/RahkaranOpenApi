using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RahkaranOpenApi.Data.Contracts;
using RestSharp;

namespace RahkaranOpenApi.Data.Commons
{
    public class RestSharpConfig : ICustomeRestRequest
    {
        private readonly RestClient _restClient;

        public RestSharpConfig(string baseUrl = "http://185.39.182.221/sg")
        {
            _restClient = new RestClient(baseUrl);
        }

        public async Task<IRestResponse<TResponse>> Post<TRequest, TResponse>(string url, TRequest input, params KeyValuePair<string, string>[] headers)
        {
            var request = new RestRequest(url, Method.POST);
            request.AddHeaders(headers);

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            var body = JsonConvert.SerializeObject(input, jsonSettings);

            request.AddJsonBody(body);

            var response = await _restClient.ExecuteAsync<TResponse>(request);
            return response;
        }

        public async Task<IRestResponse<TResponse>> Get<TRequest, TResponse>(string url, TRequest input, params KeyValuePair<string, string>[] headers)
        {

            var queryStringParams = string.Empty;
            if (headers != null && headers.Count() > 0)
            {
                var properties = input.GetType().GetProperties();
                foreach (var prop in properties)
                    queryStringParams += $"{prop.Name}={prop.GetValue(input)}&";

            }

            url = $"{url}?{queryStringParams}";

            var request = new RestRequest(url, Method.GET);
            request.AddHeaders(headers);

            var response = await _restClient.ExecuteAsync<TResponse>(request);

            return response;
        }
    }
}
