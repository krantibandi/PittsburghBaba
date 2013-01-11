using System.Linq;

namespace PittsburghBabaTemple.Core.Data
{
    public interface IFetchingStrategy<T>
    {
        IQueryable<T> Fetch(IQueryable<T> t);
    }
}
