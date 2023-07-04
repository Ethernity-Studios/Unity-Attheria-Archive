using System;

namespace SaveSystem.WorldSettings
{
    [Serializable]
    public class WorldSettings
    {
        public string WorldName;

        public string TestField;
        public int TestFieldInt;
    }

    public static class DefaultWorldSettings
    {
        public static readonly string WorldName = "Attheria";

        public static readonly string TestField = "HAHA";
        public static readonly int TestFieldInt = 0;
    }
}