namespace core.Data;

public interface IPersistence<T>
{
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    void Add(T item);
    void Update(T item);
    void Delete(Guid id);
}

