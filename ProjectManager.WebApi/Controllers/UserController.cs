using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using ProjectManager.Persistence;
using System.Data.Entity;
using NBench;
using NBench.Util;

namespace ProjectManager.WebApi.Controllers
{
    public class UserController : BaseAPIController
    {
        private Counter _counter;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }
        // GET api/values
        [SwaggerOperation("GetAllUser")]
        [PerfBenchmark(
        NumberOfIterations = 3, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public HttpResponseMessage Get()
        {
            if (_counter != null)
                _counter.Increment();
            return ToJson(ProjectManagerDB.Users.AsEnumerable());
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            return ToJson(ProjectManagerDB.Users.Where(dr => dr.User_ID == id));
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Post([FromBody]User value)
        {
            ProjectManagerDB.Users.Add(value);
            return ToJson(ProjectManagerDB.SaveChanges());
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put(int id, [FromBody]User value)
        {
            if (ProjectManagerDB.Entry(value).State == EntityState.Added)
                ProjectManagerDB.Entry(value).State = EntityState.Modified;
            return ToJson(ProjectManagerDB.SaveChanges());
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Delete(int id)
        {

            ProjectManagerDB.Users.Remove(ProjectManagerDB.Users.FirstOrDefault(x => x.User_ID == id));
            return ToJson(ProjectManagerDB.SaveChanges());
        }
        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
