using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class FeatureModel : IFeature
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
    }
}
