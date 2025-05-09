namespace Kitchen.Repository
{
    public interface IRepository<T>
    {
         List<T> GetAll(string includes = "");
        T GetById(int id);
        void Add(T obj);
        void Update(T obj);

        void Delete(int id);

        void Save();
    }
}
