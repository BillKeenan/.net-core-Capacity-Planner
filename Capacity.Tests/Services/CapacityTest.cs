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
    using Moq;

    public class CapacityServiceTests : RavenTestDriver,IDisposable {
       
       public IDocumentStoreHolder getHolder(){
               var mockHolder = new Mock<IDocumentStoreHolder>();

                mockHolder.SetupGet(x=> x.Store).Returns(GetDocumentStore());

                return mockHolder.Object;
       }

        [Fact]
        public void CanStoreProjectTest () {
           var docStore = getHolder();

            var sampleProject = new Project { name = "Hello world!" };
            using (var store = docStore.Store) {

                var service = new CapacityService (docStore);

                service.StoreProject (ref sampleProject);

                //WaitForUserToContinueTheTest (store); //Sometimes we want to debug the test itself, this redirect us to the studio

                WaitForIndexing (docStore.Store);
                using (var session = docStore.Store.OpenSession ()) {
                    var loadedProject = session.Load<Project> (sampleProject.Id);
                    Assert.NotNull (loadedProject);
                }
            }
        }

        [Fact]
        public void CanStorePersonTest () {
                     var docStore = getHolder();


            var samplePerson = new Person { firstName = "bob" };

                var service = new CapacityService (docStore);

                service.StorePerson (ref samplePerson);
                //WaitForUserToContinueTheTest (store); //Sometimes we want to debug the test itself, this redirect us to the studio

                WaitForIndexing (docStore.Store);
                using (var session = docStore.Store.OpenSession ()) {
                    var loadedPerson = session.Load<Person> (samplePerson.Id);
                    Assert.NotNull (loadedPerson);
            }
        }

        [Fact]
        public void CanAssignPersonToWork () {
        
                     var docStore = getHolder();

            var samplePerson = new Person { firstName = "bob" };
            var sampleProject = new Project { name = "Hello world!" };
            using (var store = docStore.Store) {

                using (var session = docStore.Store.OpenSession ()) {
                    session.Store (samplePerson);
                    session.Store (sampleProject);
                    session.SaveChanges ();
                    WaitForIndexing (store);


                }

                var service = new CapacityService (docStore);

                service.assignToProject (samplePerson, sampleProject, 2018, 1, 30);
                WaitForIndexing (store);

                //Sometimes we want to debug the test itself, this redirect us to the studio

                using (var session = docStore.Store.OpenSession ()) {
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
                     var docStore = getHolder();
        
            var samplePerson = new Person { firstName = "bob" };
            var sampleProject = new Project { name = "Hello world!" };
            var sampleProject2 = new Project { name = "Another Project" };
            using (var store = GetDocumentStore ()) {

                using (var session = docStore.Store.OpenSession ()) {
                    session.Store (samplePerson);
                    session.Store (sampleProject);
                    session.Store (sampleProject2);
                    session.SaveChanges ();
                    WaitForIndexing (store);


                }

                var service = new CapacityService (docStore);

                service.assignToProject (samplePerson, sampleProject, 2018, 1, 30);
                WaitForIndexing (docStore.Store);
                service.assignToProject (samplePerson, sampleProject, 2018, 2, 80);
                WaitForIndexing (docStore.Store);
                service.assignToProject (samplePerson, sampleProject2, 2018, 10, 70);
                WaitForIndexing (docStore.Store);

                //Sometimes we want to debug the test itself, this redirect us to the studio

                using (var session = docStore.Store.OpenSession ()) {
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