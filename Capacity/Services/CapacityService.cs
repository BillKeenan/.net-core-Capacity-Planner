using System;
using System.Collections.Generic;
using System.Linq;
using bigmojo.net.capacity.api.Model;
using bigmojo.net.capacity.Interface;
using Raven.Client.Documents;

namespace bigmojo.net.capacity.Services {
    public class CapacityService : ICapacity {
        private IDocumentStore store;

        public CapacityService (IDocumentStoreHolder storeHolder) {
            this.store = storeHolder.Store;
        }

        //Load the capacity % for an individual project, on week# for a certain # of weeks
        public int[] GetCapacity (int startWeek, int numberOfWeeks, Project project) {
            var caps = new List<int> ();
            Random rnd2 = new Random ();
            for (var i = 0; i < numberOfWeeks; i++) {
                caps.Add (rnd2.Next (100));
            }

            return caps.ToArray ();
        }

        public void StoreProject (ref Project project) {
            using (var session = store.OpenSession ()) {
                session.Store (project);
                session.SaveChanges ();
            }
        }

        public void StorePerson (ref Person person) {
            using (var session = store.OpenSession ()) {
                session.Store (person);
                session.SaveChanges ();
            }
        }

        public void assignToProject (Person person, Project project, int year, int week, int percentage) {
            using (var session = store.OpenSession ()) {
                //load the assignment
                var query = session.Query<Assignment> ().Where (x=> x.Person.Id == person.Id && x.Year == year).ToList();

                Assignment assignment = null;
                
                if (query.Count == 0){
                    assignment = new Assignment(person,year);
                }else if(query.Count >1){
                    throw new Exception("too many assignments!");
                }else{
                    assignment = query.First();
                }

                assignment.addAssignment(project,week,percentage);

                session.Store(assignment);
                session.SaveChanges ();
                
                
            }
        }
    }
}