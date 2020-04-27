using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.Data;
using There.Model;

namespace There.Services
{
    public class EfCoreRepository : IRepsoitory<Student>
    {
        private readonly DataContext _Context;

        public EfCoreRepository(DataContext Context)
        {
            _Context = Context;
        }
        public void Add(Student newModel)
        {
            _Context.Students.Add(newModel);
            _Context.SaveChanges();
        }

        public Student ADD(Student newModel)
        {

            _Context.Students.Add(newModel);
            _Context.SaveChanges();
            return newModel;
        }

        public IEnumerable<Student> GetAll()
        {
            return _Context.Students.ToList();
        }

        public Student GetByid(int id)
        {
            return _Context.Students.Find(id);
        }
    }
}
