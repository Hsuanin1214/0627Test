using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Models.DTOModels.Account
{
    public class LoginAccountInputDto
    {
        public string Account { get; set; }
        public string PassWord { get; set; }
    }
}
