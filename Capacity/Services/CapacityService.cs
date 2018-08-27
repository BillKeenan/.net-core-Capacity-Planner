using System;
using System.Collections.Generic;
using bigmojo.net.capacity.api.Model;
using bigmojo.net.capacity.Interface;
using Raven.Client.Documents;

namespace bigmojo.net.capacity.Services {
    public class CapacityService : ICapacity {

        //Load the capacity % for an individual project, on week# for a certain # of weeks
        public int[] GetCapacity (int startWeek, int numberOfWeeks, Project project) {
            var caps = new List<int> ();
            Random rnd2 = new Random ();
            for (var i = 0; i < numberOfWeeks; i++) {
                caps.Add (rnd2.Next (100));
            }

            return caps.ToArray ();
        }
    }
}