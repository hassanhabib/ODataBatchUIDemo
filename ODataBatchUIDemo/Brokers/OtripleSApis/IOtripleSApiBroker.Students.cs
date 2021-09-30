using System.Collections.Generic;
using System.Threading.Tasks;
using ODataBatchUIDemo.Models.Requests;
using ODataBatchUIDemo.Models.Responses;
using ODataBatchUIDemo.Models.Students;

namespace ODataBatchUIDemo.Brokers.OtripleSApis
{
    public partial interface IOtripleSApiBroker
    {
        ValueTask<List<Student>> GetAllStudentsAsync();
        ValueTask<BatchResponses> PostBatchStudentsAsync(BatchRequests batchRequests);
    }
}
