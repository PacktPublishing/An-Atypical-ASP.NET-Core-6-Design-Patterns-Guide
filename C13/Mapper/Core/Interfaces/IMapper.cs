namespace Core.Interfaces;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource entity);
}
