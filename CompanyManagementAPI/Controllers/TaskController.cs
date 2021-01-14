using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyManagementBusinessLayer;
namespace CompanyManagementAPI.Controllers
{
    [Route("api/task")]
    public class TaskController : ApiController
    {
        BusinessManager businessManager = new BusinessManager();
        [HttpPost]
        [Route("api/task/technology/update")]
        public HttpResponseMessage UpdateTechnologiesForTask(List<int> technologyIDs, int taskID)
        {
            businessManager.UpdateTechnologiesForTask(technologyIDs, taskID);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }
    }
}
