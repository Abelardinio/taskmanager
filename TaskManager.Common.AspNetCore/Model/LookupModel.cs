using TaskManager.Core;

namespace TaskManager.Common.AspNetCore.Model
{
    public class LookupModel : ILookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}