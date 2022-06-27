using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Models.ViewModels.Account.Data
{
    public class SignUpDataModel//寫attribute 驗證
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Password { get; set; }

        public string PasswordCheck { get; set; }
    }
}
