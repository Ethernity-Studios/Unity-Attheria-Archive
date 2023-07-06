using System;
using System.Collections.Generic;
using System.Linq;
using Tommy;
using UnityEngine;

namespace SaveSystem.WorldSettings {
    public class TomlLoader: MonoBehaviour {
        static object readValue(TomlNode node, Type clazz) {
            if (clazz == typeof(string)) {
                return node.AsString.ToString();
            }
            if (clazz == typeof(int)) {
                return (int)node.AsInteger.Value;
            }
            if (clazz == typeof(long)) {
                return node.AsInteger.Value;
            }

            if (clazz == typeof(float)) {
                return (float)node.AsFloat.Value;
            }

            if (clazz == typeof(double)) {
                return node.AsFloat.Value;
            }

            return clazz.Name switch
            {
                "List" => (from TomlNode o in node.AsArray
                    select Convert.ChangeType(readValue(node, clazz.GenericTypeArguments[0]), clazz.GenericTypeArguments[0])).ToList(),
                "Dictionary" => readDictionary(node, clazz),
                _ => readObject(node, clazz)
            };
        }
        
        static object readDictionary(TomlNode node, Type clazz) {
            var buffer = new Dictionary<string, object>();

            foreach (var item in node.AsTable.RawTable.Keys) {
                var tomlValue = node.AsTable.RawTable[item];

                buffer.Add(item, readValue(tomlValue, clazz.GenericTypeArguments[1]));
            }

            return buffer;
        }

        static object readObject(TomlNode node, Type clazz) {
            var obj = Activator.CreateInstance(clazz);

            foreach (var field in clazz.GetFields()) {
                if (!node.HasKey(field.Name)) {
                    field.SetValue(obj, null);
                    continue;
                }
                field.SetValue(obj, readValue(node[field.Name], field.FieldType));
            }

            return obj;
        }

        public static T readValue<T>(TomlNode node) {
            return (T) Convert.ChangeType(readValue(node, typeof(T)), typeof(T));
        }
        
        public static T readSegment<T>(TomlNode node, string name) {
            var tomlObject = node[name].AsTable;
            
            var clazz = typeof(T);

            var obj = readObject(tomlObject, clazz);

            return (T)Convert.ChangeType(obj, clazz);
        }
    }
}