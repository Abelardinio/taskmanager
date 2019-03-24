using System.Linq;
using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class ProjectsFilter : IUnsortableFilter<IProject>
    {
        public string Value { get; set; }
        public IQueryable<IProject> Filter(IQueryable<IProject> input)
        {
            return input.Where(x => string.IsNullOrEmpty(Value) || x.Name.Contains(Value));
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}