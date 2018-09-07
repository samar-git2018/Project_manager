using Newtonsoft.Json;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ProjectManager.WebApi.Controllers
{
    public class ParentTaskController : ApiController
    {
        private readonly IParentTaskRepository _parentTaskRepository;
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
        public ParentTaskController()
        : this(new ParentTaskRepository(new ProjectManagerEntities()))
        {
            //Empty constructor.
        }
        //For Testability
        public ParentTaskController(IParentTaskRepository repository)
        {
            _parentTaskRepository = repository;
        }
        // GET api/values
        [SwaggerOperation("GetAll")]
        public HttpResponseMessage Get()
        {
            return ToJson(_parentTaskRepository.Get());
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            return ToJson(_parentTaskRepository.GetByID(id));
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Post([FromBody]Parent_Task value)
        {
            //ProjectManagerDB.Parent_Task.Add(value);
            return ToJson(_parentTaskRepository.Post(value));
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put(int id, Task value)
        {
            //ProjectManagerDB.Entry(value).State = EntityState.Modified;
            return ToJson(_parentTaskRepository.Put(id, value));
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Delete(int id)
        {
            //ProjectManagerDB.Parent_Task.Remove(ProjectManagerDB.Parent_Task.FirstOrDefault(x => x.Parent_ID == id));
            return ToJson(_parentTaskRepository.Delete(id));
        }
    }
}
