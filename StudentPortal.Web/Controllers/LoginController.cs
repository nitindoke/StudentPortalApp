using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        public LoginController(ApplicationDbContext _applicationDbContext)
        {
            DbContext = _applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddUserModelClass userModel)
        {
            var _user = DbContext.Users.FirstOrDefault(u => u.UserName == userModel.UserName);
            if (_user != null)
            {
                TempData["UserName"] = userModel.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //byte[] pwd = Convert.FromBase64String(userModel.Password.ToString());
                var user = new Users
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userModel.UserName,
                    //Password = pwd[0].ToString()
                    Password = userModel.Password
                };
                await DbContext.Users.AddAsync(user);
                DbContext.SaveChanges();
            }
            return RedirectToAction("List", "Students");
            //return RedirectToAction("UserList", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var data = DbContext.Users.ToList();
            return View(data);
        }

    }
}
