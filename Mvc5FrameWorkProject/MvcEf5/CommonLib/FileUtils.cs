using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Hosting;

namespace CommonLib
{
    /// <summary>
    /// 文件处理方法
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// 解析相对路径
        /// </summary>
        /// <param name="path">相对根目录的路径</param>
        /// <returns>物理路径</returns>
        public static string MapPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            if (!VirtualPathUtility.IsAppRelative(path))
            {
                throw new DirectoryNotFoundException("无法解析非根路径");
            }

            var physicalPath = VirtualPathUtility.ToAbsolute(path, "/");
            physicalPath = physicalPath.Replace('/', '\\');
            physicalPath = physicalPath.Substring(1);
            physicalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, physicalPath);

            return physicalPath;
        }

        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        ///// <summary>
        ///// 微信证书路径
        ///// </summary>
        ///// <param name="urlPath">路径</param>
        ///// <param name="manager_id">数字ID</param>
        ///// <returns></returns>
        //public static string ExtFileName(string urlPath, int? manager_id)
        //{
        //    LogHelper.WriteInfoLog("=================判断证书是否存在==============manager_id：" + manager_id);
        //    LogHelper.WriteInfoLog("urlPath：" + urlPath);
        //    string fileName = Path.GetFileName(urlPath);
        //    string fileExt = System.IO.Path.GetExtension(urlPath);
        //    string savePath = string.Format("C:\\cert\\wdc" + "\\Content\\piclibrary\\UserImg_{0}\\", manager_id);
        //    LogHelper.WriteInfoLog("savePath：" + savePath);
        //    string saveName = savePath + fileName;
        //    LogHelper.WriteInfoLog("saveName：" + saveName);
        //    if (!System.IO.File.Exists(saveName))//判断文件是否存在
        //    {
        //        if (!System.IO.Directory.Exists(savePath))
        //            Directory.CreateDirectory(savePath);
        //        WebClient wc = new WebClient();
        //        wc.DownloadFile(urlPath, saveName);
        //        wc.Dispose();
        //    }
        //    return saveName;
        //}
    }
}