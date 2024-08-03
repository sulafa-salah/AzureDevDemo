using Newtonsoft.Json;

namespace AzureCosomDb.Demo.Models
{
    public class TodoItem
    {
    
        public string id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Category { get; set; }
    }
}

