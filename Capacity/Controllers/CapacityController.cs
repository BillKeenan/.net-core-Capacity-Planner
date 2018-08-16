using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bigmojo.net.capacity.api.Model;
using bigmojo.net.capacity.api.ViewModel;
using bigmojo.net.capacity.Services;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace react.Controllers {
    [Route ("api/[controller]")]
    public class CapacityController : Controller {

        [HttpGet ("{id}", Name = "GetCapacity")]
        public int[] GetCapacity () {

            int[] caps = {3,45,234,65};

            return caps;

        }

    }
}