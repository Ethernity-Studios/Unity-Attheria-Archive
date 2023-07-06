using System;

namespace SaveSystem.WorldSettings
{
    [Serializable]
    public class WorldSettings
    {
        
        public World world;
        public SomeSettings someSettings;
        
        [Serializable]
        public class World
        {
            public string WorldName;
            public string MapName;
        }

        [Serializable]
        public class SomeSettings
        {
            public string TestField;
            public int TestFieldInt;
        }
    }

    public static class DefaultWorldSettings
    {
        public static readonly string WorldName = "Default World";
        public static readonly string MapName = "Attheria";

        public static readonly string TestField = "HAHA";
        public static readonly int TestFieldInt = 0;
    }
}