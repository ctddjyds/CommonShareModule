﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CTDDJYDS.CommonModule
{
    public class ProcessOperate
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        public static void Kill(dynamic excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口   

            int k = 0;
            GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k  
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用  
            p.Kill();     //关闭进程k  

        }
    }
}
