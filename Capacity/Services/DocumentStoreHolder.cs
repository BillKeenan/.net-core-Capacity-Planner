using System;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;

namespace bigmojo.net.capacity.Services {
    public class DocumentStoreHolder : IDocumentStoreHolder {
        public DocumentStoreHolder (IOptions<RavenSettings> ravenSettings) {
            var settings = ravenSettings.Value;

            Store = new DocumentStore {

                Urls =  new string[] {settings.Url},
                Database = settings.DefaultDatabase
            }.Initialize ();
        }

        public IDocumentStore Store { get; }
    }

    public class RavenSettings {
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
    }
}