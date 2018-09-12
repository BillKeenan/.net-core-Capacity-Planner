using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bigmojo.net.capacity.api.Model;
using bigmojo.net.capacity.api.ViewModel;
using Raven.Client.Documents.Session;
using bigmojo.net.capacity.Services;
using Raven.Client.Documents;

namespace react.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IDocumentStore store;

        public PersonController(IDocumentStoreHolder storeHolder){
            this.store = storeHolder.Store;
        }


        [HttpGet]
        public string Test()
        {
            return "hi there";

        }

        [HttpPost]
        public IActionResult SavePerson ([FromBody] PersonViewModel personViewModel) {
            if (ModelState.IsValid) {

                Person modelModel = personViewModel;

                // Each DocumentStore needs to be initialized before use.
                using (IDocumentSession session = store.OpenSession ()) // Open a session for a default 'Database'
                {

                    session.Store (modelModel); // Assign an 'Id' and collection (Categories)
                    // and start tracking an entity

                    session.SaveChanges (); // Send to the Server
                    // one request processed in one transaction
                }
                return Ok();
            }

            return BadRequest("bad person");

        }


        [HttpGet("{id}", Name = "GetPerson")]
        public Microsoft.AspNetCore.Mvc.JsonResult GetById(string id)
        {
            return Json(new Person{firstName="bill"});
            

        }

    }
}
