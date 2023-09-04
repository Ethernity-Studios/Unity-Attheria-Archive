using System;
using UnityEngine;

namespace GolbyUtils
{
    public static class TransformUtil
    {
        public static float ClampAngle(float angle, float from, float to)
        {
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max(angle, 360 + from);
            return Mathf.Min(angle, to);
        }
    }

    public static class TimeUtil
    {
        public static string FormatTime(double time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            return $"{timeSpan.Days}d{timeSpan.Hours}h{timeSpan.Minutes}m{timeSpan.Seconds}s";
        }
    }
}