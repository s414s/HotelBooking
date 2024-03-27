namespace Domain.Contracts;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T? GetByID(Guid id);
    T Add(T entity);
    bool Delete(T entity);
    T Update(T entity);
    void SaveChanges();
}
