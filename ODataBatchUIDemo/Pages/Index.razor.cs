using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ODataBatchUIDemo.Brokers.OtripleSApis;
using ODataBatchUIDemo.Models.Requests;
using ODataBatchUIDemo.Models.Students;
using Syncfusion.Blazor.Grids;

namespace ODataBatchUIDemo.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IOtripleSApiBroker ApiBroker { get; set; }

        public List<Student> Students { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Students =
                await this.ApiBroker.GetAllStudentsAsync();
        }

        public async Task OnBatchSave(BeforeBatchSaveArgs<Student> args)
        {
            var batchRequests = new BatchRequests();
            batchRequests.Requests = new List<Request>();

            foreach(Student student in args.BatchChanges.AddedRecords)
            {
                batchRequests.Requests.Add(new Request
                {
                    Id = Guid.NewGuid().ToString(),

                    Headers = new Header
                    {
                        ContentType = "application/json"
                    },

                    Method = Method.POST,
                    Body = student,
                    Url = "students"
                });
            }

            foreach (Student student in args.BatchChanges.ChangedRecords)
            {
                batchRequests.Requests.Add(new Request
                {
                    Id = Guid.NewGuid().ToString(),

                    Headers = new Header
                    {
                        ContentType = "application/json"
                    },

                    Method = Method.PUT,
                    Body = student,
                    Url = "students"
                });
            }

            foreach (Student student in args.BatchChanges.DeletedRecords)
            {
                batchRequests.Requests.Add(new Request
                {
                    Id = Guid.NewGuid().ToString(),

                    Headers = new Header
                    {
                        ContentType = "application/json"
                    },

                    Method = Method.DELETE,
                    Body = student,
                    Url = $"students/{student.Id}"
                });
            }

            await this.ApiBroker.PostBatchStudentsAsync(batchRequests);
        }
    }
}
