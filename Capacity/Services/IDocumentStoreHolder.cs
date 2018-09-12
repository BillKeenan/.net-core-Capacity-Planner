using Raven.Client.Documents;

namespace bigmojo.net.capacity.Services
{
    public interface IDocumentStoreHolder
    {
            IDocumentStore Store { get; }

    }
}