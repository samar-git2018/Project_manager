using NBench;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using Swashbuckle.Swagger.Annotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Text;

namespace ProjectManager.WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected HttpResponseMessage ToJson(dynamic obj)
        {
            var response = Request.CreateResponse();
            if (obj == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                return response;
            }
        }
        public UserController()
        : this(new UserRepository(new ProjectManagerEntities()))
        {
            //Empty constructor.
        }
        //For Testability
        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }
        //private Counter _counter;

        //[PerfSetup]
        //public void Setup(BenchmarkContext context)
        //{
        //    _counter = context.GetCounter("TestCounter");
        //}

        //[PerfBenchmark(
        //NumberOfIterations = 3, RunMode = RunMode.Throughput,
        //RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        //[CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
        //[MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        //[GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        //public void PerfGet(BenchmarkContext context)
        //{
        //    Get();
        //    if (_counter != null)
        //        _counter.Increment();
        //}

        // GET api/values
        [SwaggerOperation("GetAllUser")]
        public HttpResponseMessage Get()
        {
            return ToJson(_userRepository.Get());
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            return ToJson(_userRepository.GetByID(id));
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Post([FromBody]User value)
        {
            return ToJson(_userRepository.Post(value));
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put(int id, [FromBody]User value)
        {
            return ToJson(_userRepository.Put(id, value));
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Delete(int id)
        {

            return ToJson(_userRepository.Delete(id));
        }
        //[PerfCleanup]
        //public void Cleanup()
        //{
        //    // does nothing
        //}
    }
}
