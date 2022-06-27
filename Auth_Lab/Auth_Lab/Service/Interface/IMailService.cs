using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Lab.Service.Interface
{
    public interface IMailService//跟信有關//註冊信//寄信//驗證信
    {
        public void SendVerifyMail(string mailTo , int userId );//寄給誰，對應哪個userid

    }
}
