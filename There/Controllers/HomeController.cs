using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.Model;
using There.Services;
using There.ViewModels;

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

            var vms = list.Select(x => new StudentViewModel
            {
                //大括号里面可以放变量
                //格式化字符串 类似于formate
                Name = $"{x.FirstName}{x.LastName}",
               Age = DateTime.Now.Subtract(x.BirthDate).Days / 365,
              id= x.id,
            });

            var vm = new HomeIndexViewModel
            {
                StudentViews = vms
            };
            return View(vm);
        }
        #endregion


        public IActionResult Detail(int id)
        {
            var  student = _repsoitory.GetByid(id);
            if(student==null)
            {
                return RedirectToAction("index","home");
            }
            return View(student);
        }


        //加了Authorize 只能登录的用户才能访问
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //这样写studetn里面有啥就要写啥
        //[HttpPost]
        //public IActionResult Create(Student  student)
        //{
        //    return Content(JsonConvert.SerializeObject(student));
        //}

        //ValidateAntiForgeryToken 过滤特性，该性表示检测服务器请求是否被修改，这个特性只能用于post请求，get请求无效
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentCreateViewModel student)
        {
            if (ModelState.IsValid) //判断验证是否通过
            {
                var newStudent = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender
                };
                var newmodel = _repsoitory.ADD(newStudent);
                //这样写刷新的时候会提交数据
                //return View("Detail",newmodel);

                //重定向的写法
                // return RedirectToAction("create","home");

                //这样写有利于重构
                return RedirectToAction(nameof(Detail), new { id = newmodel.id });
            }
            else
            {
                return View();
            }
           
        }
    }
}
