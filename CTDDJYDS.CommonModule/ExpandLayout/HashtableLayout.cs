﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Layout;
using log4net.Layout.Pattern;
using System.Reflection;
using System.Collections;

namespace TGLog.ExpandLayout
{
    public class HashtableLayout : PatternLayout
    {
        public HashtableLayout()
        {
            this.AddConverter("property", typeof(HashtablePatternConverter));
        }
    }

    public class HashtablePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {

            if (Option != null)
            {
                // Write the value for the specified key
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }

        /// <summary>
        /// 通过哈希表键得到值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="loggingEvent"></param>
        /// <returns></returns>
        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            Hashtable ht = new Hashtable();
            if (loggingEvent.MessageObject.GetType().Equals(ht.GetType()))
            {
                propertyValue = ((Hashtable)loggingEvent.MessageObject)[property];
            }
            else
            {
                PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
                if (propertyInfo != null)
                {
                    propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
                }
            }
            return propertyValue;
        }


        ///// <summary>
        ///// 通过反射获取传入的日志对象的某个属性的值
        ///// </summary>
        ///// <param name="property"></param>
        ///// <returns></returns>
        //private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        //{
        //    object propertyValue = string.Empty;

        //    PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
        //    if (propertyInfo != null)
        //    {
        //        propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
        //    }
        //    return propertyValue;
        //}
    }
}
