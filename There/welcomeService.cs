using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace There
{
    public class welcomeService : IwelcomeService
    {
        public string GetMessAge()
        {
            return "hello world ";
        }
    }
}
