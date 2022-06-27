using Auth_Lab.Models.DTOModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Service.Interface
{
    public interface IAccountService
    {
        public CreateAccountOutputDto CreateAccount(CreateAccountInputDto input);
        public LoginAccountOutputDto LoginAccount(LoginAccountInputDto input);
        public void LogoutAccount();
        public bool IsExistAccount(string email);//用email去判斷
        public void VerifyAccount(int userid);//用userid驗證
    }
}
