using Newtonsoft.Json;

namespace Application_Insight_MVC.Controllers;

public class ToDoItemClass
{
    public class ToDoItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        // The ToString() method is used to format the output, it's used for demo purpose only. It's not required by Azure Cosmos DB
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}