using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.IRepository
{
    public interface IAllRepository<T> where T : class
    {
        public ICollection<T> GetAll();
        public T GetById(dynamic id);
        public bool CreateOjb(T ojb);
        public bool UpdateOjb(T ojb);
        public bool DeleteOjb(T ojb);
    }
}
