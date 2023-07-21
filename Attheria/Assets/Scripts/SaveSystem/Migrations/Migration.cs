using System;
using UnityEngine;

namespace SaveSystem.Migrations
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MigrationAttribute : Attribute
    {
        public string test;
        public float testFloat;

        public void LogMe()
        {
            Debug.Log("TestLog");

        }
    }
}