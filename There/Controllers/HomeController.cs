using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.Model;

namespace There.Controllers
{
    public class HomeController:Controller
    {
        public string indexThere()
        {
            return "hello form HomeController ";
            
        }

        //返回的是文字
        //public IActionResult index()
        //{
        //    return this.Content("i you ok");
        //}


        //r返回的是一个json
        //public IActionResult Index()
        //{
        //    var std = new Student
        //    {
        //        id = 1,
        //        FirstName = "ok",
        //        LastName = "canert"

        //};
        //    return new ObjectResult(std);
        //}

            

            //返回视图

        public IActionResult index()
        {
            var std = new Student
            {
                id = 1,
                FirstName = "ok",
                LastName = "canert"

            };
            return View(std);
        }
    }
}
