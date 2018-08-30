using System;
using System.IO;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;
using Xunit;

namespace Capacity.Tests {
    using System.Linq;
    using bigmojo.net.capacity.api.Model;
    using bigmojo.net.capacity.Services;
    using Raven.Client.Documents.Indexes;
    using Raven.Client.Documents;
    using Raven.TestDriver;
    using Xunit;
    using Xunit.Sdk;

        
    public class CapacityServiceTests : RavenTestDriver,IDisposable {
       

        [Fact]
        public void CanStoreProjectTest () {
           

            var sampleProject = new Project { name = "Hello world!" };
            using (var store = GetDocumentStore ()) {

                var service = new CapacityService (store);

                service.StoreProject (ref sampleProject);

                //WaitForUserToContinueTheTest (store); //Sometimes we want to debug the test itself, this redirect us to the studio

                WaitForIndexing (store);
                using (var session = store.OpenSession ()) {
                    var loadedProject = session.Load<Project> (sampleProject.Id);
                    Assert.NotNull (loadedProject);
                }
            }
        }

        [Fact]
        public void CanStorePersonTest () {
          

            var samplePerson = new Person { firstName = "bob" };
            using (var store = GetDocumentStore ()) {

                var service = new CapacityService (store);

                service.StorePerson (ref samplePerson);
                //WaitForUserToContinueTheTest (store); //Sometimes we want to debug the test itself, this redirect us to the studio

                WaitForIndexing (store);
                using (var session = store.OpenSession ()) {
                    var loadedPerson = session.Load<Person> (samplePerson.Id);
                    Assert.NotNull (loadedPerson);
                }
            }
        }

        [Fact]
        public void CanAssignPersonToWork () {
        

            var samplePerson = new Person { firstName = "bob" };
            var sampleProject = new Project { name = "Hello world!" };
            using (var store = GetDocumentStore ()) {

                using (var session = store.OpenSession ()) {
                    session.Store (samplePerson);
                    session.Store (sampleProject);
                    session.SaveChanges ();
                    WaitForIndexing (store);


                }

                var service = new CapacityService (store);

                service.assignToProject (samplePerson, sampleProject, 2018, 1, 30);
                WaitForIndexing (store);

                //Sometimes we want to debug the test itself, this redirect us to the studio

                using (var session = store.OpenSession ()) {
                    //WaitForUserToContinueTheTest (store);
                    var query =
                        session.Query<Assignment> ().
                    Where (x => x.Person.Id == samplePerson.Id && x.Year == 2018);
                    Assert.Single (query);

                }

            }

            
        }


        [Fact]
        public void AssigningSecondDoesntDestroyOthers () {
        
            var samplePerson = new Person { firstName = "bob" };
            var sampleProject = new Project { name = "Hello world!" };
            var sampleProject2 = new Project { name = "Another Project" };
            using (var store = GetDocumentStore ()) {

                using (var session = store.OpenSession ()) {
                    session.Store (samplePerson);
                    session.Store (sampleProject);
                    session.Store (sampleProject2);
                    session.SaveChanges ();
                    WaitForIndexing (store);


                }

                var service = new CapacityService (store);

                service.assignToProject (samplePerson, sampleProject, 2018, 1, 30);
                WaitForIndexing (store);
                service.assignToProject (samplePerson, sampleProject, 2018, 2, 80);
                WaitForIndexing (store);
                service.assignToProject (samplePerson, sampleProject2, 2018, 10, 70);
                WaitForIndexing (store);

                //Sometimes we want to debug the test itself, this redirect us to the studio

                using (var session = store.OpenSession ()) {
                    //WaitForUserToContinueTheTest (store);
                    var query =
                        session.Query<Assignment> ().
                    Where (x => x.Person.Id == samplePerson.Id && x.Year == 2018);
                    Assert.Equal (1,query.Count());
                    Assert.Equal(30,query.First().Assignments[sampleProject.Id][1]);
                    Assert.Equal(80,query.First().Assignments[sampleProject.Id][2]);
                    Assert.Equal(70,query.First().Assignments[sampleProject2.Id][10]);

                }

            }

            
        }

    }

    public class TestDocumentByName : AbstractIndexCreationTask<TestDocument> {
        public TestDocumentByName () {
            Map = docs => from doc in docs select new { doc.Name };
            Indexes.Add (x => x.Name, FieldIndexing.Search);
        }
    }

    public class TestDocument {
        public string Name { get; set; }
    }
}