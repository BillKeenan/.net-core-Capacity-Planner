using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bigmojo.net.capacity.api.model;

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
        public string TestPost([ FromBody] Project project)
        {
             if (ModelState.IsValid)
            {
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
