using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bigmojo.net.capacity.api.model;
using bigmojo.net.capacity.api.Model;
using bigmojo.net.capacity.api.ViewModel;
using bigmojo.net.capacity.Services;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace react.Controllers {
    [Route ("api/[controller]")]
    public class ProjectController : Controller {

        [HttpGet]
        public ProjectViewModel[] GetProjects () {

            List<ProjectViewModel> vms = new List<ProjectViewModel> ();

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

        }

        [HttpPost]
        public string SaveProject ([FromBody] ProjectViewModel project) {
            if (ModelState.IsValid) {

                Project modelModel = project;

                // Each DocumentStore needs to be initialized before use.
                using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession ()) // Open a session for a default 'Database'
                {

                    session.Store (modelModel); // Assign an 'Id' and collection (Categories)
                    // and start tracking an entity

                    session.SaveChanges (); // Send to the Server
                    // one request processed in one transaction
                }
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