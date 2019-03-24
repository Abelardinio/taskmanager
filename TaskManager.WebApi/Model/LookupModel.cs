using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class LookupModel : ILookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}