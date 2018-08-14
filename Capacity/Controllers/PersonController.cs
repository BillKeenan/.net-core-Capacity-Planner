using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bigmojo.net.capacity.api.model;

namespace react.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {


        [HttpGet]
        public string Test()
        {
            return "hi there";

        }

        [HttpPost]
        public string SaveProject ([FromBody] PersonViewModel personViewModel) {
            if (ModelState.IsValid) {

                Person modelModel = personViewModel;

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


        [HttpGet("{id}", Name = "GetPerson")]
        public Microsoft.AspNetCore.Mvc.JsonResult GetById(string id)
        {
            return Json(new Person{firstName="bill"});
            

        }

    }
}
