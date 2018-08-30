using System;
using System.IO;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;
using Xunit;

namespace Capacity.Tests {
    using System.Linq;
    using Raven.Client.Documents.Indexes;
    using Raven.Client.Documents;
    using Raven.TestDriver;
    using Xunit;

    public class RavenEchoTest : RavenTestDriver {
        //This allows us to modify the conventions of the store we get from 'GetDocumentStore'
        protected override void PreInitialize (IDocumentStore documentStore) {
            documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;
        }

        [Fact]
        public void RavenProofTest () {
            ConfigureServer (new TestServerOptions {
                DataDirectory = "/tmp"
            });

            using (var store = GetDocumentStore ()) {
                store.ExecuteIndex (new TestDocumentByName ());
                using (var session = store.OpenSession ()) {
                    session.Store (new TestDocument { Name = "Hello world!" });
                    session.Store (new TestDocument { Name = "Goodbye..." });
                    session.SaveChanges ();
                }
                WaitForIndexing (store); //If we want to query documents sometime we need to wait for the indexes to catch up
                //WaitForUserToContinueTheTest (store); //Sometimes we want to debug the test itself, this redirect us to the studio
                using (var session = store.OpenSession ()) {
                    var query = session.Query<TestDocument, TestDocumentByName> ().Where (x => x.Name == "hello").ToList ();
                    Assert.Single (query);
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

}