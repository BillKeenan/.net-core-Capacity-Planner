using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bigmojo.net.capacity.api.Model;
using bigmojo.net.capacity.api.ViewModel;
using bigmojo.net.capacity.Interface;
using bigmojo.net.capacity.Services;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace react.Controllers {
    [Route ("api/[controller]")]
    public class CapacityController : Controller {
        private readonly ICapacity capcityService;

        public CapacityController(ICapacity capcityService){
            this.capcityService = capcityService;
        }

        
        [HttpGet ("{id}/{start}/{count}", Name = "GetCapacity")]
        public int[] GetCapacity (string id,int start,int count) {

            return capcityService.GetCapacity(start,count,new Project{name=id});

        }

    }
}