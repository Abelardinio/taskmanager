namespace TaskManager.Core
{
    public interface IFeatureInfo
    {
        int ProjectId { get; }
        string Name { get; }
        string Description { get; }
    }
}