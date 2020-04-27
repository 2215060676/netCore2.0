using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using There.Model;
using There.Services;

namespace There.ViewComponents
{
    public class WelcomeViewComponent:ViewComponent
    {

        private readonly IRepsoitory<Student> _repsoitory;
        public WelcomeViewComponent(IRepsoitory<Student> repsoitory)
        {
            _repsoitory = repsoitory;
        }

        public IViewComponentResult invoke()
        {
            var count = _repsoitory.GetAll().Count();
            return View("default", count);
        }
    }
}
