using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public string GetById(string id)
        {
            return Json(new Person{firstname="bill"});
            

        }

    }
}
