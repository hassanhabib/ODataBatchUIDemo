using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ODataBatchUIDemo.Models.Configurations;
using RESTFulSense.Clients;

namespace ODataBatchUIDemo.Brokers.OtripleSApis
{
    public partial class OtripleSApiBroker : IOtripleSApiBroker
    {
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiClient;

        public OtripleSApiBroker(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiClient = GetApiClient(configuration);
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        public async ValueTask<TResult> PostAsync<T, TResult>(string relativeUrl, T content) =>
            await PostContentAsync<T,TResult>(relativeUrl, content);

        private RESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfiguration localConfiguration =
                configuration.Get<LocalConfiguration>();

            string otripleSApiUrl = localConfiguration.ApiUrl;
            this.httpClient.BaseAddress = new Uri(otripleSApiUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }

        public async ValueTask<TResult> PostContentAsync<TContent, TResult>(string relativeUrl, TContent content)
        {
            StringContent contentString = StringifyJsonifyContent(content);

            HttpResponseMessage responseMessage =
               await this.httpClient.PostAsync(relativeUrl, contentString);

            return await DeserializeResponseContent<TResult>(responseMessage);
        }

        private static StringContent StringifyJsonifyContent<T>(T content)
        {
            string serializedRestrictionRequest = JsonConvert.SerializeObject(content);

            var contentString =
                new StringContent(serializedRestrictionRequest, Encoding.UTF8, "application/json");

            return contentString;
        }

        private static async ValueTask<T> DeserializeResponseContent<T>(HttpResponseMessage responseMessage)
        {
            string responseString = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
