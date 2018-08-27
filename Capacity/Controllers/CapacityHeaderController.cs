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
    public class CapacityHeaderController : Controller {
        private readonly ICapacity capcityService;

        public CapacityHeaderController(ICapacity capcityService){
            this.capcityService = capcityService;
        }

        [HttpGet ("{start}/{count}", Name = "GetCapacityHeader")]
        public string[] GetCapacityHeader (int start,int count) {

            var day1 = Utility.FirstDateOfWeekISO8601(DateTime.Now.Year,start);

            var dates = new List<String>();
            for (var i = 0; i <= count; i++){
                dates.Add(day1.AddDays(7 * i).ToShortDateString());
            }

            return dates.ToArray();
        }

    }
}