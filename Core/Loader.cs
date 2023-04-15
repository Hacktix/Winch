using HarmonyLib;
using System;
using System.Reflection;

namespace Winch.Core
{
    public class Loader
    {
        public static void Main()
        {
            var harmony = new Harmony("com.dredge.winch");
            harmony.PatchAll();
        }
    }
}
