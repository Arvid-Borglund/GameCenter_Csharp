namespace GameCenter.Interfaces
{
    internal interface ICRUDRepository<T>
    {
        void Create(T entity);
        T GetById(String id);
        List<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
        T GetByCompositeId(String id, DateTime dateTime);
    }

}
