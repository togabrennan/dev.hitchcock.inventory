using core.Domain;

namespace core.Data;

public class RdbmsStore<T> : IPersistence<T> where T : class, IHasId
{
    public void Add(T item)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(T item)
    {
        throw new NotImplementedException();
    }
}

