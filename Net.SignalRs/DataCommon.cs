using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.SignalRs
{
    public class DataCommon
    {
        /// <summary>
        /// 传输超时时间 
        /// </summary>
        public static int ClientConnectTimeout { get; set; } = 40000;
    }
}
