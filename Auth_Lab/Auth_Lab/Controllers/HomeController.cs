using Auth_Lab.Models;
using Auth_Lab.Models.DBEntity;
using Auth_Lab.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDBRepository _iDBRepository;
        public HomeController(ILogger<HomeController> logger, IDBRepository dBRepository)
        {
            _logger = logger;
            _iDBRepository = dBRepository;
        }

        public IActionResult Index()
        {
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

        public IActionResult NoAuthPage()
        {
            return View();
        }
        [Authorize]
        public IActionResult AuthPage()
        {
            //userid 在這裡登入
            var userId = int.Parse(User.Identity.Name);
            var addressData = _iDBRepository.GetAll<AddressBook>().Where(x => x.UserId == userId).ToList(); 
            
            return View(addressData);
        }
        //要特定腳色才能去的葉面
        [Authorize(Roles ="Admin")]
        public IActionResult RolePage()
        {
            return View();
        }
    }
}
