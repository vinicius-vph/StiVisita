namespace StiVisita.Interfaces
{
    interface IService<T>
    {
        public List<T> FindAll();
        public T FindById(int? id);
        public void Create(T obj);
        public void Update(T obj);
        public void Delete(int? id);
    }
}
