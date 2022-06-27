using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Models.DTOModels.Account
{
    public class CreateAccountInputDto//註冊
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string PasswordCheck { get; set; }//再次確認密碼欄位
    }
}
