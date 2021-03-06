﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CTDDJYDS.CommonModule
{
    public class FileDirectoryOperate
    {
        public static bool DeleteFileWithTime(string fileName, uint timeout = 0)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            try
            {
                if (timeout <= 0)
                {
                    File.Delete(fileName);
                }
                else if (timeout > 0)
                {
                    while (timeout-- > 0)
                    {
                        try
                        {
                            File.Delete(fileName);
                            break;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Log(ex);
                        }
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                LogHelper.Log(ex);
                return false;
            }
        }
        public static void OpenDirectory(string dir)
        {
            if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
            {
                return;
            }
            // add the quotation mark to make sure it's working when name contains comma
            string args = string.Format("/e, \"{0}\"", dir);
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.Arguments = args;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.Start();
            p.WaitForExit(100);
            p.Close();
        }

        public static bool CreateDirectoryEx(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                {
                    List<string> drivers = new List<string>(Directory.GetLogicalDrives());
                    string root = Directory.GetDirectoryRoot(dir).ToUpper();

                    if (drivers.Contains(root) || Directory.Exists(root))
                    {
                        Directory.CreateDirectory(dir);
                        return Directory.Exists(dir);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception e)
            {
                LogHelper.Log(e);
                return false;
            }
        }

        /// <summary>
        /// 获取文件的绝对路径,针对window程序和web程序都可使用
        /// </summary>
        /// <param name="relativePath">相对路径地址</param>
        /// <returns>绝对路径地址</returns>
        public static string GetAbsolutePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                throw new ArgumentNullException("参数relativePath空异常！");
            }
            relativePath = relativePath.Replace("/", "\\");
            if (relativePath[0] == '\\')
            {
                relativePath = relativePath.Remove(0, 1);
            }
            //判断是Web程序还是window程序
            if (HttpContext.Current != null)
            {
                return Path.Combine(HttpRuntime.AppDomainAppPath + "\\bin", relativePath);
            }
            else
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\bin", relativePath);
            }
        }
    }
}
