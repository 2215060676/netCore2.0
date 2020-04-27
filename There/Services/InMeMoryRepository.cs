using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.Model;

namespace There.Services
{
    public class InMeMoryRepository : IRepsoitory<Student>
    {


        private readonly List<Student> _Students;


        public InMeMoryRepository()
        {
            _Students = new List<Student>
            {
                new Student
                {
                    id=1,
                    FirstName="01",
                    LastName="01",
                    BirthDate=new DateTime(2018,10,10),
                },
                 new Student
                {
                    id=2,
                    FirstName="02",
                    LastName="02",
                     BirthDate=new DateTime(2018,10,10),
                },
                  new Student
                {
                    id=3,
                    FirstName="04",
                    LastName="04",
                     BirthDate=new DateTime(2018,10,10),
                 },
             };
        }

        public void Add(Student newModel)
        {
            var MaxId = _Students.Max(x => x.id);
            newModel.id = MaxId +1;
            _Students.Add(newModel);
        }

        public Student ADD(Student newModel)
        {
            var MaxId = _Students.Max(x => x.id);
            newModel.id = MaxId + 1;
            _Students.Add(newModel);
            return newModel;
        }

        public IEnumerable<Student> GetAll()
        {
            return _Students;
        }

        public Student GetByid(int id )
        {
            return _Students.FirstOrDefault(x=>x.id==id);
        }
    }
}
