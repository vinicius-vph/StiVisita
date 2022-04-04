using System.Collections;

namespace StiVisita.Interfaces
{
    public interface IRepository
    {
        public List<Hashtable> Get();
        public Hashtable Get(int? id);
        public void Create(Hashtable param);
        public void Update(Hashtable param);
        public void Destroy(int? id);
    }
}
