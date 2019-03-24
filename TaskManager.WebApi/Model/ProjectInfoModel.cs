using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class ProjectInfoModel : IProjectInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}