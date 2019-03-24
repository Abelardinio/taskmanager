namespace TaskManager.Core
{
    public interface IFeature : IFeatureInfo
    {
        int Id { get; }
    }
}