using System;

namespace SaveSystem.SaveLoad
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = false)]
    public class SaveFileAttribute : Attribute
    {
        public string FileName;

        public SaveFileAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}