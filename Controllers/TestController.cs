using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheSecret.Models;



namespace TheSecret.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly ApplicationDBContext _context;

        public TestController(ApplicationDBContext context)
        {
            _context = context;
        }
       // ApplicationDBContext db = new ApplicationDBContext();

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Secret()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User userdata)
        {
            //if(userdata)
            //{
            //    RedirectToAction("Signup");
            //}
            if (ModelState.IsValid)
            {
                //var obj = db.Users.Where(m => m.UserID == user.UserID && m.Password == user.Password).FirstOrDefault();
                bool IsUserExist = false;

                User user = await _context.Users.FindAsync(userdata .UserID);

                if (user != null)
                {
                    IsUserExist = true;
                }
                else
                {
                    user = new User();
                }
                try
                {
                    user.UserID = userdata.UserID;
                    user.FirstName = userdata.FirstName;
                    user.LastName = userdata.LastName;
                    user.Password = userdata.Password;
                    user.EmailID = userdata.EmailID;

                    if (IsUserExist)
                    {
                        _context.Update(user);
                    }
                    else
                    {
                        _context.Add(user);
                    }
                    await _context.SaveChangesAsync();
                    //_context.Add(user);
                    //var result = _context.SaveChanges();
                }

                
               
                    catch (DbUpdateConcurrencyException)
                {
                    throw;
                   // return RedirectToAction("Signup");
                }
               
                
               
                    
                
            }
            return View();
        }

      
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if(ModelState.IsValid)
            {
                var obj =  _context.Users.Where(m => m.UserID == user.UserID && m.Password == user.Password).FirstOrDefault();
                if(obj!=null)
                {

                    return RedirectToAction("Secret");
                }
            }
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
