﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Script.Serialization;

namespace RhoconnectNET
{
    public class Helpers
    {
        public static Func<String, String, Hashtable, bool> _auth_handler;

        public static bool rhoconnect_authenticate(String username, String password, Hashtable attrs)
        {
            if (_auth_handler != null)
            {
                return _auth_handler.Invoke(username, password, attrs);
            }

            return true;
        }

        public static ActionResult serialize_result(object id)
        {
            return new ContentResult
            {
                Content = id.ToString(),
                ContentType = "text/plain"
            };
        }

        public static Object deserialize_json(String objJson, Type targetType)
        {
            return new JavaScriptSerializer().Deserialize(objJson, targetType);
        }

        public static void merge_changes(Object obj, Dictionary<string, object> changes)
        {
            foreach (KeyValuePair<string, object> kvp in changes)
            {
                PropertyInfo property = obj.GetType().GetProperty(kvp.Key);
                Object convValue = Convert.ChangeType(kvp.Value, property.PropertyType);
                property.SetValue(obj, convValue, null);
            }
        }
    }
}