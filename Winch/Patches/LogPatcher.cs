using HarmonyLib;
using System.Diagnostics;
using UnityEngine;
using Winch.Config;
using Winch.Core;

namespace Winch.Patches
{
    [HarmonyPatch(typeof(Logger))]
    [HarmonyPatch("GetString")]
    class LogPatcher
    {
        private static string GetLogSource()
        {
            StackFrame[] frames = new StackTrace().GetFrames();
            string callingClass = "";
            string callingMethod = "";
            string callingAssembly = "";
            for (int i = frames.Length - 1; i > 0; i--)
            {
                if (frames[i].GetMethod().Name == "Log")
                    break;
                callingMethod = frames[i].GetMethod().Name;
                callingClass = frames[i].GetMethod().ReflectedType.Name;
                callingAssembly = frames[i].GetMethod().ReflectedType.Assembly.GetName().Name;
            }

            string sourceTag = $"{callingAssembly}";
            if (WinchConfig.GetProperty("DetailedLogSources", false))
                sourceTag += $".{callingClass}.{callingMethod}";

            return sourceTag;
        }

        static void Postfix(object message)
        {
            WinchCore.Log.Unity(message, GetLogSource());
        }
    }
}
