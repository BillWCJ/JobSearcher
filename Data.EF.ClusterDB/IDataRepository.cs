namespace Data.EF.ClusterDB
{
    public interface IDataRepository<T> where T : class
    {
        T GetById(int id);
        T Add(T entity);

    }
}