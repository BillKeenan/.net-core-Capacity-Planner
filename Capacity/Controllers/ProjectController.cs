using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using bigmojo.net.capacity.api.Model;
using bigmojo.net.capacity.api.ViewModel;
using bigmojo.net.capacity.Interface;
using bigmojo.net.capacity.Services;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;

namespace react.Controllers {
    [Route ("api/[controller]")]
    public class ProjectController : Controller {
        private readonly ICapacity capcityService;

        public ProjectController(ICapacity capcityService){
            this.capcityService = capcityService;
        }
        [HttpGet]
        public ActionResult<ProjectViewModel[]> GetProjects () {

            List<ProjectViewModel> vms = new List<ProjectViewModel> ();

            try {
                // Each DocumentStore needs to be initialized before use.
                using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession ()) // Open a session for a default 'Database'
                {

                    List<Project> projectsx = session
                        .Query<Project> ()
                        .ToList (); // Send to the Server

                    Console.Write ("got projects:" + projectsx.Count);
                    projectsx.ForEach (delegate (Project proj) {
                        vms.Add (proj);
                    }); // one request processed in one transaction
                }
                return vms.ToArray ();

            }catch(RavenException e){
                    return StatusCode(500,"Data base Connection Failed:"+e.Message);
            }
        }

        [HttpPost]
        public string SaveProject ([FromBody] ProjectViewModel project) {
            if (ModelState.IsValid) {

                Project modelModel = project;

               //capcityService.
                return "posted project:" + modelModel.name;
            }

            return "bad project:";

        }

        [HttpGet ("{id}", Name = "GetProject")]
        public string GetById (string id) {
            return "hi";

        }

    }
}