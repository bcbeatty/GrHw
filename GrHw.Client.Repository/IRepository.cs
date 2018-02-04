namespace GrHw.Client.Repository
{
    public interface IRepository<T,K> where T:class
    {
        T GetByKey(K key);
        T Update(T item);
        T Insert(T item);
        void Delete(T item);

    }
}