using System;

namespace SaveSystem.Migrations
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MigrationAttribute : Attribute
    {
        public string formerName;
        public Type formerType;
    }
}