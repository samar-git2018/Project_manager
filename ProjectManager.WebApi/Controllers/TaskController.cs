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
    public class TaskController : ApiController
    {
        private readonly ITaskRepository _taskRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected HttpResponseMessage ToJson(dynamic obj)
        {
            var response = Request.CreateResponse();
            response.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            if (obj == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }
        public TaskController()
        : this(new TaskRepository(new ProjectManagerEntities()))
        {
            //Empty constructor.
        }
        //For Testability
        public TaskController(ITaskRepository repository)
        {
            _taskRepository = repository;
        }
        // GET api/values
        [SwaggerOperation("GetAll")]
        public HttpResponseMessage Get()
        {
            //var data = ProjectManagerDB.sp_GetTaskData();
            return ToJson(_taskRepository.Get());
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            return ToJson(_taskRepository.GetByID(id));
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Post([FromBody]Task value)
        {
            //var data = ProjectManagerDB.sp_InsertTaskUpdateUser(value.Parent_ID, value.TaskName, value.Start_Date, value.End_Date, value.Priority, value.Status, value.User_ID, value.Project_ID);
            //ProjectManagerDB.Tasks.Add(value);
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, data);
            return ToJson(_taskRepository.Post(value));
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put(int id, Task value)
        {
            //ProjectManagerDB.Entry(value).State = EntityState.Modified;
            return ToJson(_taskRepository.Put(id, value));
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Delete(int id)
        {
            //ProjectManagerDB.Tasks.Remove(ProjectManagerDB.Tasks.FirstOrDefault(x => x.Task_ID == id));
            return ToJson(_taskRepository.Delete(id));
        }
    }
}
