﻿using Coldairarrow.DotNettyRPC;
using Common;
using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            RPCServer rPCServer = new RPCServer(39999);
            rPCServer.RegisterService<IHello, Hello>();
            rPCServer.Start();

            Console.ReadLine();
        }
    }
}
