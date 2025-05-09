namespace Kitchen.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T obj);
        void Edit(T obj);
        void Delete(int id);
        void Save();
    }
}
