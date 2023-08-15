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

            public bool AllZonesUnlocked;
        }

        [Serializable]
        public class Player
        {
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
        /// <summary>
        /// World
        /// </summary>
        public static readonly string WorldName = "Default World";
        public static readonly string MapName = "Attheria";
        public static readonly bool AllZonesUnlocked = false;

        /// <summary>
        /// SomeSettings
        /// </summary>
        public static readonly string TestField = "HAHA";
        public static readonly int TestFieldInt = 0;
    }
}