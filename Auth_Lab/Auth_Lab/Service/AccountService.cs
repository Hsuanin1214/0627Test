using Auth_Lab.Common;
using Auth_Lab.Models.DBEntity;
using Auth_Lab.Models.DTOModels.Account;
using Auth_Lab.Repository.Interface;
using Auth_Lab.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth_Lab.Service
{
    public class AccountService : IAccountService
    {
        private readonly IDBRepository _iDBRepository;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IDBRepository iDBRepository, IMailService mailService, IHttpContextAccessor httpContextAccessor)
        {
            _iDBRepository = iDBRepository;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public CreateAccountOutputDto CreateAccount(CreateAccountInputDto input)
        {
            var result = new CreateAccountOutputDto();
            result.IsSuccess = false;
            result.User.UserName = input.Name;
            result.User.UserEmail= input.Email;
            result.User.UserPhone = input.Phone;
            //檢核
            if (this.IsExistAccount(input.Email))
            {
                result.Message = "Email已經存在";
                return result;
            }

            if(input.Password != input.PasswordCheck)
            {
                result.Message = "密碼、確認密碼不正確";
                return result;
            }
            //mapping
            var entity = new User
            {
                Email = input.Email,
                Name = input.Name,
                Phone = input.Phone,
                Password = Encryption.SHA256Encrypt(input.Password),//將create 資料要進行加密 //工程師也不能知道加密
                IsAdmin = false,//預設false
                IsVerify = false//預設false
            };
            var target = _iDBRepository.authDbContext.Users.Add(entity);
            _iDBRepository.Save();

            //如何驗證?
            //簡單寄信服務
            _mailService.SendVerifyMail(target.Entity.Email, target.Entity.Id);



            result.IsSuccess = true;
            result.User.UserId = target.Entity.Id;

            return result;
        }


        public LoginAccountOutputDto LoginAccount(LoginAccountInputDto input)
        {
            var result = new LoginAccountOutputDto();
            result.IsSuccess = false;
            //檢核
            if (!this.IsExistAccount(input.Account))
            {
                result.Message = "使用者帳號不存在";
                return result;
            }
            var currentUser = _iDBRepository.GetAll<User>().First(x => x.Email == input.Account);

            if (!currentUser.IsVerify)
            {
                result.Message = "使用者帳號尚未驗證";
                return result;
            }
            if(Encryption.SHA256Encrypt(input.PassWord) != currentUser.Password) //比對加密後的結果
            {
                result.Message = "密碼錯誤";
                return result;
            }
            result.IsSuccess = true;
            result.User.UserId = currentUser.Id;
            result.User.UserName = currentUser.Name;
            result.User.UserPhone = currentUser.Phone;
            result.User.UserEmail = currentUser.Email;
            result.User.UserRole = currentUser.IsAdmin ? "Admin" : "User";
            if (result.IsSuccess)
            {
                //安全性 Claim//聲明
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.User.UserId.ToString()),
                    new Claim(ClaimTypes.Email, result.User.UserEmail.ToString()),
                    new Claim(ClaimTypes.Role, result.User.UserRole.ToString()),
                    new Claim("UserName", result.User.UserName.ToString())//可以自訂
                };
                //如果要自己打，要跟startup一樣
                //很像cookie的名字
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //如果準則驗證成功，請她幫你登入//?甚麼意思//cookie會加密
                _httpContextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));
            }

            return result;
        }

        public void LogoutAccount()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void VerifyAccount(int userid)//驗證帳號
        {
            var user = _iDBRepository.GetAll<User>().First(x => x.Id == userid);
            if(!user.IsVerify)
            {
                user.IsVerify = true;

                _iDBRepository.Update<User>(user);
                _iDBRepository.Save();
            }
            
        }

        public bool IsExistAccount(string email)
        {
            return _iDBRepository.GetAll<User>().Any(x => x.Email == email);
        }
    }
}
