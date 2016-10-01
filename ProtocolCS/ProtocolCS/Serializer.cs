﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ProtocolCS
{
    public class Serializer
    {
        private static JsonSerializerSettings defaultSerializerSetting
        {
            get
            {
                JsonSerializerSettings setting = new JsonSerializerSettings();
                setting.TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                setting.TypeNameHandling = TypeNameHandling.All;

                return setting;
            }
        }

        public static string ToJson<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj is null");

            return JsonConvert.SerializeObject(
                obj,
                Formatting.None,
                defaultSerializerSetting);
        }

        public static object ToObject(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException("json is null/empty");

            return JsonConvert.DeserializeObject<T>(
                json,
                defaultSerializerSetting);
        }
        public static T ToObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException("json is null/empty");

            return JsonConvert.DeserializeObject<T>(
                json,
                defaultSerializerSetting);
        }
    }
}
