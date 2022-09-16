using DataCollection.Models;

namespace DataCollection.PubSub
{
    public interface ISubscriber
    {
        public abstract void Callback(IDataModel data);
    }
}
