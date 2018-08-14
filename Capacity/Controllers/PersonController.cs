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
        public string TestPost()
        {
            return "posted";

        }


        [HttpGet("{id}", Name = "GetPerson")]
        public Microsoft.AspNetCore.Mvc.JsonResult GetById(string id)
        {
            return Json(new Person{firstName="bill"});
            

        }

    }
}
