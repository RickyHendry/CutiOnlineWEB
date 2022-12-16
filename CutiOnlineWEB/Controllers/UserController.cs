using CutiOnlineWEB.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Controllers
{
    public class UserController : Controller
    {
        CrudRepository CrudRepository;

        public UserController(CrudRepository CrudRepository)
        {
            this.CrudRepository = CrudRepository;
        }
        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("Role").Equals("Admin"))
            {
                var data = CrudRepository.Get();
                return View();
            }
            return RedirectToAction("Unauthorized", "ErrorPage");   
        }
    }
}
