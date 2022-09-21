using DataCollection.Models;

namespace DataCollection.PubSub
{
    public interface ISubscriber
    {
        public void Callback(IDataModel data);
    }
}
