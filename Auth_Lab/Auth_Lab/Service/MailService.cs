using Auth_Lab.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Auth_Lab.Service
{
    public class MailService : IMailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MailService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public void SendVerifyMail(string mailTo, int userId)
        {
            //寄信服務
            //SMTP
            //網域 EX. @gmail.com  通過那個mail server //smtp.server 587 port指令
            //80 port http
            //443 port https
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            /*client.Credentials = new NetworkCredential("bsdevteam01@gmail.com","bs53741405P@ssw0rd");//google的信箱的帳號密碼*/
            client.Credentials = new NetworkCredential("BestStudy666666@gmail.com", "qbksaydrijzfcgdo");
            client.EnableSsl = true;

            //Mail Message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("bsdevteam01@gmail.com", "BestStudy");
            mail.To.Add(mailTo);//mail.To本身是一個集合
            mail.Priority = MailPriority.Normal;//重不重要的信 Low重要性低
            mail.Subject = "BestStudy_2022_註冊驗證信";
            mail.IsBodyHtml = true;//預設是true//要讓它變成html
            mail.Body = @$"
                <h1>點擊以下連結已啟用帳戶</h1>
                <a href='https://{_httpContextAccessor.HttpContext.Request.Host.Value}' target='_blank'>連結</a>
            ";//點擊這個網址可以連結//訊息內容可以寫html//測試才能寫死
            client.Send(mail);
            //<a href='https://localhost:5001/Account/Verify?user={userId}' target='_blank'>連結</a>
        }
    }
}
