using System;
using System.Collections.Generic;
using System.Linq;
using Tommy;

namespace SaveSystem.WorldSettings {
    public static class TomLoader {
        static TomlNode writeObject(object obj, Type type) {
            var node = new TomlTable();

            foreach (var field in type.GetFields()) {
                node[field.Name] = writeValue(field.GetValue(obj), field.FieldType);
            }

            return node;
        }

        static TomlNode writeDictionary(Dictionary<string, object> dictionary, Type valueType) {
            var node = new TomlTable();
            
            foreach (var item in dictionary) {
                node.Add(item.Key, writeValue(item.Value, valueType));
            }

            return node;
        }

        static TomlNode writeArray(IEnumerable<object> iterable, Type itemType) {
            var node = new TomlArray();

            foreach (var item in iterable) {
                node.Add(writeValue(item, itemType));
            }

            return node;
        }

        public static TomlNode writeValue<T>(T value) {
            return writeValue(value, typeof(T));
        }

        static TomlNode writeValue(object value, Type clazz) {
            if (clazz == typeof(string)) {
                return new TomlString
                {
                    Value = (string)Convert.ChangeType(value, TypeCode.String)
                };
            }
            if (clazz == typeof(int)) {
                return new TomlInteger
                {
                    Value = (int)Convert.ChangeType(value, TypeCode.Int32)
                };
            }
            if (clazz == typeof(long)) {
                return new TomlInteger
                {
                    Value = (long)Convert.ChangeType(value, TypeCode.Int64)
                };
            }
            if (clazz == typeof(float)) {
                return new TomlFloat
                {
                    Value = (double)Convert.ChangeType(value, TypeCode.Double)
                };
            }
            if (clazz == typeof(double)) {
                return new TomlFloat
                {
                    Value = (double)Convert.ChangeType(value, TypeCode.Double)
                };
            }

            if (clazz == typeof(bool))
            {
                return new TomlBoolean
                {
                    Value = (bool)Convert.ChangeType(value, TypeCode.Boolean)
                };
            }

            return clazz.Name switch
            {
                "Dictionary" => writeDictionary((Dictionary<string, object>)value, clazz.GenericTypeArguments[1]),
                "List" => writeArray((IEnumerable<object>)value, clazz.GenericTypeArguments[0]),
                _ => writeObject(value, clazz)
            };
        }
        
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

            if (clazz == typeof(bool))
            {
                return node.AsBoolean.Value;
            }

            return clazz.Name switch
            {
                "List" => (from TomlNode o in node.AsArray
                    select Convert.ChangeType(readValue(node, clazz.GenericTypeArguments[0]),
                        clazz.GenericTypeArguments[0])).ToList(),
                "Dictionary" => readDictionary(node, clazz),
                _ => readObject(node, clazz)
            };
        }

        static object readDictionary(TomlNode node, Type clazz) => node.AsTable.RawTable.ToDictionary(item => item.Key,
            item => readValue(item.Value, clazz.GenericTypeArguments[1]));

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

        public static T readValue<T>(TomlNode node) => (T)Convert.ChangeType(readValue(node, typeof(T)), typeof(T));

        public static T readSegment<T>(TomlNode node, string name) {
            var tomlObject = node[name].AsTable;

            var clazz = typeof(T);

            var obj = readObject(tomlObject, clazz);

            return (T)Convert.ChangeType(obj, clazz);
        }
    }
}