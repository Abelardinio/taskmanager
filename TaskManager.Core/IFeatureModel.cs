namespace TaskManager.Core
{
    public interface IFeatureModel : IFeature
    {
        string ProjectName { get; }
    }
}