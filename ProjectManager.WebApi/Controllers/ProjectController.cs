using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using ProjectManager.WebApi.Repository;
using ProjectManager.Persistence;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Text;

namespace ProjectManager.WebApi.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController()
        : this(new ProjectRepository(new ProjectManagerEntities()))
        {
            //Empty constructor.
        }
        //For Testability
        public ProjectController(IProjectRepository repository)
        {
            _projectRepository = repository;
        }
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
        // GET api/values
        [SwaggerOperation("GetAll")]
        public HttpResponseMessage Get()
        {
            return ToJson(_projectRepository.Get());
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            return ToJson(_projectRepository.GetByID(id));
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Post([FromBody]Project value)
        {
            return ToJson(_projectRepository.Post(value));
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put(int id, [FromBody]Project value)
        {
            return ToJson(_projectRepository.Put(id, value));
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Delete(int id)
        {
            return ToJson(_projectRepository.Delete(id));
        }
    }
}
