using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utility;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText = "hello";
            var ciphertext= SecurityHelper.AESEncrypt(plainText);
            var descryptedText = SecurityHelper.AESDecrypt(ciphertext);
            Console.WriteLine($"原始明文:{plainText}");
            Console.WriteLine($"加密后文本:{ciphertext}");
            Console.WriteLine($"解密后文本:{descryptedText}");
            Console.ReadLine();
        }
    }
}
