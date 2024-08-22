namespace BenchTimeWebService.Database
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> Add(T entity);

        public Task<T> Update(string id, T entity);

        public Task Delete(string id);

        public Task<T> Get(string id);
    }
}
