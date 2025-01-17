﻿namespace Misc.Console
{
    public static class ConsoleFormatter
    {
        public static string Bold(this string str) => "<b>" + str + "</b>";
        public static string Color(this string str,string clr) => $"<color={clr}>{str}</color>";
        public static string Italic(this string str) => "<i>" + str + "</i>";
        public static string Size(this string str, int size) => $"<size={size}>{str}</size>";
    }
}