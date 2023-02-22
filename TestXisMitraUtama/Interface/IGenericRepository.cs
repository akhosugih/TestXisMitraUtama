namespace TestXisMitraUtama.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetList();
        Task<T> Get(long id);
        Task<bool> Insert (T data);
        Task<bool> Update (T data);
        Task<bool> Delete (long id);
    }
}
