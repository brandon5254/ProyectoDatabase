using ProyectoCrudMongoDB.Data;
using ProyectoCrudMongoDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoCrudMongoDB.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();

        }


        [HttpPost]

        public async Task<IActionResult> Index(People? user)
        {

            if (user != null)
            {
                DbUsers db = new DbUsers();
                var userExist = await db.FindUserByEmail(user.Username, user.Email, user.Password);
                if (userExist)
                {
                    ViewBag.success = false;
                    ViewBag.message = "User Already Exist";
                    return View();
                }
                else
                {
                    ViewBag.success = true;
                    await db.SaveUser(user);

                    ViewBag.status = true;
                    ViewBag.message = "User Saved Successfully";
                    return RedirectToAction("Index", "Login");

                    //return View();

                }
            }

            return View();
        }
    }




}
