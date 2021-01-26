using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skybeat.Data;
namespace skybeat.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            SkybeatContext context = HttpContext.RequestServices.GetService(typeof(skybeat.Models.UserSetting)) as SkybeatContext;

            return View(context.GetAllUsersSetting());
        }
    }
}
