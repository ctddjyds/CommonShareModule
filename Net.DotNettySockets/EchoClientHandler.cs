﻿using DotNetty.Buffers;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.DotNettySockets
{
    public class EchoClientHandler : ChannelHandlerAdapter
    {
        readonly IByteBuffer initialMessage;

        public EchoClientHandler()
        {
            this.initialMessage = Unpooled.Buffer(1024);
            byte[] messageBytes = Encoding.UTF8.GetBytes("Hello world");
            this.initialMessage.WriteBytes(messageBytes);
        }
        //private Bootstrap _bootstrap;
        //public EchoClientHandler(Bootstrap bst):base()
        //{
        //    _bootstrap = bst;
        //}
        // 新的连接建立的时候会触发
        //重写基类方法，当链接上服务器后，马上发送Hello World消息到服务端
        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
            //context.WriteAndFlushAsync(this.initialMessage); 
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as IByteBuffer;
            if (byteBuffer != null)
            {
                //Console.WriteLine("Received from server: " + byteBuffer.ToString(Encoding.UTF8));
            }
            //context.WriteAsync(message);
            context.WriteAndFlushAsync(message);//需要使用这个方法，去除远程主机强制关闭连接
        }
        /// <summary>
        /// 管道读取完成时
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            //Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }

        //ChannelHandlerAdapter 重写UserEventTriggered
        public override void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            if (evt is IdleStateEvent)
            {
                var eventState = evt as IdleStateEvent;
                if (eventState != null)
                {
                    //this._bootstrap.SendHeartbeatAsync(context, eventState);
                }
            }
        }
        public override void HandlerAdded(IChannelHandlerContext context)
        {
            //Console.WriteLine($"服务端{context}上线.");
            base.HandlerAdded(context);
        }

        public override void HandlerRemoved(IChannelHandlerContext context)
        {
            //Console.WriteLine($"服务端{context}下线.");
            base.HandlerRemoved(context);
        }
    }
}
