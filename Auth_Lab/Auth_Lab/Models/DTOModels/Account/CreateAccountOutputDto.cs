using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Models.DTOModels.Account
{
    public class CreateAccountOutputDto
    {
        public CreateAccountOutputDto()
        {
            User = new UserData();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserData User{ get; set; }

        public class UserData//註冊好的基本資訊
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserPhone { get; set; }
            public string UserEmail { get; set; }
        }
    }
}
