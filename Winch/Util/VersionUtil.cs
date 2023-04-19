using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Winch.Core;

namespace Winch.Util
{
    internal class VersionUtil
    {
        private static readonly string Prefix = "alpha";
        private static readonly string[] ValidPrefixes = new string[] { "alpha", "beta", null };
        private static Regex VersionRegex = new Regex(@"(?:([a-z]+)-)?(\d+)\.(\d+)", RegexOptions.Compiled);

        internal static string GetVersion()
        {
            string versionString = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string[] parts = versionString.Split('.');

            return $"{Prefix}-{parts[0]}.{parts[1]} | build {parts[2]}";
        }

        internal static string GetComparableVersion()
        {
            string versionString = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string[] parts = versionString.Split('.');

            return $"{Prefix}-{parts[0]}.{parts[1]}";
        }

        internal static bool ValidateVersion(string version)
        {
            Match match = VersionRegex.Match(version);
            if (!match.Success)
                return false;

            string prefix = match.Groups[1].Value;
            if(!ValidPrefixes.Contains(prefix))
                return false;
            return true;
        }

        internal static bool IsSameOrNewer(string installedVersion, string minVersion)
        {
            if (!ValidateVersion(installedVersion) || !ValidateVersion(minVersion))
                throw new ArgumentException($"Invalid version comparison: {installedVersion} - {minVersion}");

            GroupCollection installedParts = VersionRegex.Match(installedVersion).Groups;
            GroupCollection minParts = VersionRegex.Match(minVersion).Groups;

            int prefixIndexDiff = Array.IndexOf(ValidPrefixes, installedParts[1].Value) - Array.IndexOf(ValidPrefixes, minParts[1].Value);
            int majorDiff = int.Parse(installedParts[2].Value) - int.Parse(minParts[2].Value);
            int minorDiff = int.Parse(installedParts[3].Value) - int.Parse(minParts[3].Value);

            if (prefixIndexDiff < 0) return false;
            else if(prefixIndexDiff > 0) return true;
            else
            {
                if(majorDiff < 0) return false;
                else if(majorDiff > 0) return true;
                else
                {
                    if (minorDiff < 0) return false;
                    else return true;
                }
            }
        }
    }
}
