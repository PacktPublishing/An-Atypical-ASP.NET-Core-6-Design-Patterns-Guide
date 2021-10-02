namespace Core.Interfaces
{
    public interface IMappingService
    {
        TDestination Map<TSource, TDestination>(TSource entity);
    }
}
