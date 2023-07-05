using System;
using System.Collections.Generic;
using System.Linq;
using Tommy;
using UnityEngine;

namespace SaveSystem.WorldSettings {
    public class TomlLoader: MonoBehaviour {
        static object readValue(TomlNode node, Type clazz) {
            object KYS() {
                var aids = node.AsTable.RawTable.Keys;
                var bufrik = new Dictionary<string, object>();

                foreach (var aid in aids) {
                    var hodnotka = node.AsTable.RawTable[aid];

                    bufrik.Add(aid, readValue(hodnotka, clazz.GenericTypeArguments[1]));
                }

                return bufrik;
            }
            
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
                "Dictionary" => KYS(),
                _ => O(node, clazz)
            };
        }

        private static object O(TomlNode node, Type clazz) {
            var fields = clazz.GetFields();
            var obj = Activator.CreateInstance(clazz);

            foreach (var field in fields) {
                var a = node[field.Name];
                field.SetValue(obj, readValue(a, field.FieldType));
            }

            return obj;
        }

        static T readValue<T>(TomlNode node) {
            return (T) Convert.ChangeType(readValue(node, typeof(T)), typeof(T));
        }
        
        static  T read<T>(TomlNode table, string name) {
            var autista = table[name].AsTable;
            
            var clazz = typeof(T);

            var obj = (T)Activator.CreateInstance(clazz);

            var fields = clazz.GetFields();

            foreach (var field in fields) {
                var a = autista[field.Name];
                field.SetValue(obj, readValue(a, field.FieldType));
            }

            return obj;
        }
    }
}