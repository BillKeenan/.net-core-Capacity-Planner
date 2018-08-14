using System;
using Raven.Client.Documents;

namespace bigmojo.net.capacity.Services {
    public class DocumentStoreHolder {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore> (CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore () {
            IDocumentStore store = new DocumentStore () {
                Urls = new [] { "http://localhost:8080" },
                    Database = "capacity"
            }.Initialize ();

            return store;
        }
    }
}