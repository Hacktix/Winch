using System.Reflection;

namespace Winch.Util
{
    internal class VersionUtil
    {
        private static readonly string Prefix = "alpha";

        internal static string GetVersion()
        {
            string versionString = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string[] parts = versionString.Split('.');

            return $"{Prefix}-{parts[0]}.{parts[1]} | build {parts[2]}";
        }
    }
}
