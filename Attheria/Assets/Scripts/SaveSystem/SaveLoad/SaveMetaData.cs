using System;

namespace SaveSystem.SaveLoad
{
    [Serializable]
    public struct SaveMetaData
    {
        public string SaveName;
        public float Playtime;
        public string Version;
        public long SaveDate;

        public DateTimeOffset Time
        {
            get => DateTimeOffset.FromUnixTimeMilliseconds(SaveDate);
            set => SaveDate = value.ToUnixTimeMilliseconds();
        }
    }
}