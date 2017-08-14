using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommonLib
{
    public static class EncryptUtils
    {
        #region Md5

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string Md5(string str)
        {
            var b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            return b.Aggregate(string.Empty, (current, t) => current + t.ToString("x").PadLeft(2, '0'));
        }
        /// <summary>
        /// 32位小写加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5x32(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }
        /// <summary>
        /// 32位大写加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5X32(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                pwd = pwd + s[i].ToString("X2");

            }
            return pwd;
        }

        #endregion Md5

        #region SHA

        /// <summary>
        /// SHA1加密字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Sha1(string source)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytesSha1In = Encoding.Default.GetBytes(source);
            byte[] bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            string strSha1Out = BitConverter.ToString(bytesSha1Out);
            return strSha1Out.Replace("-", "").ToLower();
        }

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string Sha256(string str)
        {
            var sha256Data = Encoding.UTF8.GetBytes(str);
            var sha256 = new SHA256Managed();
            var result = sha256.ComputeHash(sha256Data);
            return Convert.ToBase64String(result);  //返回长度为44字节的字符串
        }

        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string Sha512(string source)
        {
            var result = string.Empty;
            SHA512 sha512 = new SHA512Managed();
            var s = sha512.ComputeHash(Encoding.UTF8.GetBytes(source));
            result = s.Aggregate(result, (current, t) => current + t.ToString("X"));
            sha512.Clear();
            return result;
        }

        #endregion SHA

        #region DES

        /// <summary>
        /// des解密默认key
        /// </summary>
        private const string DesKey = "74851234";

        /// <summary>
        /// des加密
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptDes(string encryptString)
        {
            return EncryptDes(encryptString, DesKey, DesKey);
        }

        /// <summary>
        /// des解密字符串
        /// </summary>
        /// <param name="decryptString">待解密字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecryptDes(string decryptString)
        {
            return DecryptDes(decryptString, DesKey, DesKey);
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <param name="decryptIv">8位Iv</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDes(string encryptString, string encryptKey, string decryptIv)
        {
            try
            {
                var rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                var rgbIv = Encoding.UTF8.GetBytes(DesKey);
                var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var dCsp = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                StringBuilder sb = new StringBuilder();
                foreach (var b in mStream.ToArray())
                {
                    sb.AppendFormat("{0:X2}", b);
                }
                return sb.ToString();
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <param name="decryptIv">8位Iv</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDes(string decryptString, string decryptKey, string decryptIv)
        {
            try
            {
                var rgbKey = Encoding.ASCII.GetBytes(decryptKey);
                var rgbIv = Encoding.ASCII.GetBytes(decryptIv);
                var inputByteArray = new byte[decryptString.Length / 2];
                for (int x = 0; x < decryptString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                var dcsp = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        #endregion DES
    }
}