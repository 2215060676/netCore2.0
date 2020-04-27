using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace There.Services
{
    //where T 是因为要限制这个T只能是一个类而不能是别，比如不能是接口
    public interface IRepsoitory<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetByid(int id);

        void Add(T newModel);

        T ADD(T newModel);
    }
}
