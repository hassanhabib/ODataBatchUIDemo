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
            await this.apiClient.PostContentAsync<T,TResult>(relativeUrl, content, mediaType: "application/json");

        private RESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfiguration localConfiguration =
                configuration.Get<LocalConfiguration>();

            string otripleSApiUrl = localConfiguration.ApiUrl;
            this.httpClient.BaseAddress = new Uri(otripleSApiUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}
