using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth_Lab.Common
{
    public static class Encryption //加密//靜態，不用new
    {
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
       public static string SHA256Encrypt(string strSource)//加密的方法 //SHA128,SHA256,SHA....
        {
            byte[] source = Encoding.Default.GetBytes(strSource);
            byte[] crypto = new SHA256CryptoServiceProvider().ComputeHash(source);
            string result = crypto.Aggregate(string.Empty, (current, t) => current + t.ToString("X2"));//X代表16進位，每次都看兩位
            return result;
        }
    }
}
