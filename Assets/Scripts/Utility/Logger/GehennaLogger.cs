using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gehenna
{
    public class GehennaLogger
    {
        private static readonly Dictionary<LogType, string> LogColorMap = new()
        {
            { LogType.Info,    "#FFFFFF" }, // White
            { LogType.Success, "#228B22" }, // Forest green
            { LogType.Warning, "#FFFF00" }, // Yellow
            { LogType.Error,   "#FF0000" }  // Red
        };
        
        public static void Log(object caller, LogType logType, string message)
        {
            if (logType == LogType.None)
                return;
            
            if (caller == null)
                return;

            string category = caller.GetType().Name;
            string format = FormatMessage(category, logType, message);
            
            switch (logType)
            {
                case LogType.Warning: 
                    Debug.LogWarning(format); 
                    break;
                case LogType.Error: 
                    Debug.LogError(format); 
                    break;
                default: 
                    Debug.Log(format); 
                    break;
            }
        }
        
        private static string FormatMessage(string category, LogType logType, string message)
        {
            if (!LogColorMap.TryGetValue(logType, out var color))
                color = "white";

            return $"<color={color}>[{logType}/{category}]</color> {message}";
        }
    }
}