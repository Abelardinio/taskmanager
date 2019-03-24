using System.Linq;
using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class FeaturesFilter : IUnsortableFilter<IFeature>
    {
        public string Value { get; set; }
        public IQueryable<IFeature> Filter(IQueryable<IFeature> input)
        {
            return input.Where(x => string.IsNullOrEmpty(Value) || x.Name.Contains(Value));
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}