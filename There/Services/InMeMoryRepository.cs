using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.Model;

namespace There.Services
{
    public class InMeMoryRepository : IRepsoitory<Student>
    {
        public IEnumerable<Student> GetAll()
        {
            return new List<Student>
            {
                new Student
                {
                    id=1,
                    FirstName="01",
                    LastName="01",
                },
                 new Student
                {
                    id=2,
                    FirstName="02",
                    LastName="02",
                },
                  new Student
                {
                    id=3,
                    FirstName="04",
                    LastName="04",
                     },
             };
        }
    }
}
