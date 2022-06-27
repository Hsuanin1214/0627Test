using Auth_Lab.Models.DTOModels.Account;
using Auth_Lab.Models.ViewModels.Account.Data;
using Auth_Lab.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth_Lab.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult Verify(int user)
        {
            _accountService.VerifyAccount(user);
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            _accountService.LogoutAccount();
            return Redirect("/");
        }
        public IActionResult UserInfo()
        {
            return PartialView("_UserInfo");
        }

        //----------------------------------------------------------------
        [HttpPost]
        public IActionResult SignUp(SignUpDataModel request)
        {
            var inputDto = new CreateAccountInputDto
            {
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Phone = request.Phone,
                PasswordCheck = request.PasswordCheck
            };
            var outputDto = _accountService.CreateAccount(inputDto);//會得到一個outputDto
            if (outputDto.IsSuccess)
            {
                return View("CheckEmail");//導到登入首頁
            }
            else
            {
                return View("SignUp");//導回signup的View
            }
        }

        [HttpPost]
        public IActionResult Login(LoginDataModel request)
        {
            var inputDto = new LoginAccountInputDto
            {
                Account = request.Account,
                PassWord = request.Password
            };
            var outputDto = _accountService.LoginAccount(inputDto);

            if (outputDto.IsSuccess)
            {
                ////安全性 Claim//聲明
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, outputDto.User.UserId.ToString()),
                //    new Claim(ClaimTypes.Email, outputDto.User.UserEmail.ToString()),
                //    new Claim(ClaimTypes.Role, outputDto.User.UserRole.ToString()),
                //    new Claim("UserName", outputDto.User.UserName.ToString())//可以自訂//自訂欄位(聲明)
                //};
                ////如果要自己打，要跟startup一樣
                ////很像cookie的名字，認cookie的
                //var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ////如果準則驗證成功，請她幫你登入//?甚麼意思
                //HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));

                return Redirect("/");
            }
            else
            {
                return View("Login");
            }
        }
        [HttpPost]
        //[FromQuery] 從網址列請求
        //[FromBody] 從Body請求
        public IActionResult FetchLogin([FromBody]LoginDataModel request)
        {
            var inputDto = new LoginAccountInputDto
            {
                Account = request.Account,
                PassWord = request.Password
            };

            var ouputDto = _accountService.LoginAccount(inputDto);

            return new JsonResult(ouputDto);//自動轉成Json的物件
        }
    }
}
