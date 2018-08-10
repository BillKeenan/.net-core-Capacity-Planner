using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bigmojo.net.capacity.api.model;
using Raven.Client.Documents.Session;
using Raven.Client.Documents;
using bigmojo.net.capacity.api.ViewModel;
using bigmojo.net.capacity.api.Model;

namespace react.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {


        [HttpGet]
        public string Test()
        {
            return "hi there";

        }

        [HttpPost]
        public string TestPost([ FromBody] ProjectViewModel project)
        {
             if (ModelState.IsValid)
            {
              

            Project modelModel = project;

            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]                        // URL to the Server,
                {                                   // or list of URLs 
                    "http://127.0.0.1:8080"  // to all Cluster Servers (Nodes)
                },
                Database = "capacity",             // Default database that DocumentStore will interact with
            })
            {
            store.Initialize();     
                        // Each DocumentStore needs to be initialized before use.
                using (IDocumentSession session = store.OpenSession())  // Open a session for a default 'Database'
                {
                   
                    session.Store(project);                            // Assign an 'Id' and collection (Categories)
                                                                        // and start tracking an entity

                    session.SaveChanges();                              // Send to the Server
                                                                        // one request processed in one transaction
                }
            }
              return "posted project:"+project.firstName;
            }

            return "bad project:";

        }


        [HttpGet("{id}", Name = "GetProject")]
        public string GetById(string id)
        {
            return "hi";            

        }

    }
}
