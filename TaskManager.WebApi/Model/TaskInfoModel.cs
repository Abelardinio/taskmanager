namespace TaskManager.WebApi.Model
{
    public class TaskInfoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public TaskInfoTimeSpan TimeToComplete { get; set; }
        public int FeatureId { get; set; }
    }
}