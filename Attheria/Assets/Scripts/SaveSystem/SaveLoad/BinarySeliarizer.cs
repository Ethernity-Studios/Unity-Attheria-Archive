using System;
using System.CodeDom.Compiler;
using System.IO;
using Newtonsoft.Json;

namespace SaveSystem.SaveLoad {
    public class BinarySerializer {
        // f32, f64, i32, i64, byte, char
        // option<T> == byte T?
        // list<T>   == i32 T*i32
        // Pair<A, B> == A B
        
        MemoryStream s = new MemoryStream();

        /*public byte[] getBytes() {
            var buf = new byte[s.Length] { };

            s.Read(buf);

            return buf;
        }
*/
        public void writeValue<T>(T value) {
            var d = new StreamWriter(s);
            JsonSerializer.CreateDefault(null).Serialize(d, 1);
        }
    }

    public class BinaryDeSerializer {
        private byte[] data;
        
        public BinaryDeSerializer(byte[] data) {
            this.data = data;
        }
        
        public T readValue<T>() {
            return JsonConvert.DeserializeObject<T>(System.Text.Encoding.UTF8.GetString(data));
        }
    }
}