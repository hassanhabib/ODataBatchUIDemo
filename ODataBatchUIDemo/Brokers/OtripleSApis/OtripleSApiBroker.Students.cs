using System.Collections.Generic;
using System.Threading.Tasks;
using ODataBatchUIDemo.Models.Requests;
using ODataBatchUIDemo.Models.Responses;
using ODataBatchUIDemo.Models.Students;

namespace ODataBatchUIDemo.Brokers.OtripleSApis
{
    public partial class OtripleSApiBroker
    {
        private const string RelativeStudentsUrl = "api/students";

        public async ValueTask<BatchResponses> PostBatchStudentsAsync(BatchRequests batchRequests) =>
            await PostAsync<BatchRequests, BatchResponses>("api/$batch", batchRequests);

        public async ValueTask<List<Student>> GetAllStudentsAsync() =>
            await GetAsync<List<Student>>(RelativeStudentsUrl);
    }
}
