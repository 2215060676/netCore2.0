using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace There.Controllers
{

    //特性路由    [Route("[Controller]")]
    //[Route("about")]
    [Route("v2/[Controller]/[action]")] //v2表示版本
    public class AboutController : Controller
    {
       // [Route("")]
        public string Me()
        {
            return "Dave";
        }

       // [Route("Company")]
        public string Company()
        {
            return "NO Company";
        }
    }
}