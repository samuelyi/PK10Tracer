﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
namespace WolfInv.com.BaseObjectsLib
{
    public class JsonableClass<T>
    {
        public T FromJson(string strjson)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Deserialize<T>(strjson);
            }
            catch(Exception ce)
            {
                return default(T);
            }
        }

        public string ToJson()
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(this);
            }
            catch
            {
                return null;
            }
        }
    }
}
