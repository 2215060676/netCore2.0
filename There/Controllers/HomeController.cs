using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.Model;
using There.Services;

namespace There.Controllers
{
    public class HomeController:Controller
    {

        private readonly IRepsoitory<Student> _repsoitory;
        public HomeController(IRepsoitory<Student> repsoitory)
        {
            _repsoitory = repsoitory;
        }
        public string indexThere()
        {
            return "hello form HomeController ";
            
        }



        #region  //返回的是文字
        //public IActionResult index()
        //{
        //    return this.Content("i you ok");
        //}
        #endregion

        #region //r返回的是一个json
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
        #endregion

        #region//返回视图
        //public IActionResult index()
        //{
        //    var std = new Student
        //    {
        //        id = 1,
        //        FirstName = "ok",
        //        LastName = "canert"

        //    };
        //    return View(std);
        //}
        #endregion


        #region  返回一个集合
        public IActionResult index()
        {
            var list = _repsoitory.GetAll();
            return View(list);
        }
        #endregion

    }
}
