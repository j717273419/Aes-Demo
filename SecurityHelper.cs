using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
    /*
    output:
    
    原始明文:hello
    加密后文本:067816C1337F95348AC7E9AD68D6ED93
    解密后文本:hello
         
    */
    public abstract class SecurityHelper
    {
        const string AESIV = "J(biwwalk@i*v3n&";
        const string AESKEY = "mm2a)-Ls23.k0*7^a@3daQ>b";
        
        public static string AESEncrypt(string strText)
        {
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(strText);
            RijndaelManaged rm = new RijndaelManaged();
            rm.Padding = PaddingMode.PKCS7;
            rm.Mode = CipherMode.CBC;
            rm.BlockSize = 128;
            rm.KeySize = 128;
            rm.Key = ASCIIEncoding.ASCII.GetBytes(AESKEY);
            rm.IV = ASCIIEncoding.ASCII.GetBytes(AESIV);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rm.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        public static string AESDecrypt(string strText)
        {
           
            int len;
            len = strText.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(strText.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            RijndaelManaged rm = new RijndaelManaged();
            rm.Padding = PaddingMode.PKCS7;
            rm.Mode = CipherMode.CBC;
            rm.BlockSize = 128;
            rm.KeySize = 128;
            rm.Key = ASCIIEncoding.ASCII.GetBytes(AESKEY);
            rm.IV = ASCIIEncoding.ASCII.GetBytes(AESIV);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rm.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
