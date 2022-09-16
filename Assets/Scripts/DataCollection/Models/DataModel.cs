namespace DataCollection.Models
{
    public interface IDataModel: IModel
    {
        /*
        This is just to group all Models by Type for use in parameters
        
        All Models must be json serializable by Newtonsoft.JSON
        
        Data Models have no functionality and are never planned to have functionality
        */
    }
}