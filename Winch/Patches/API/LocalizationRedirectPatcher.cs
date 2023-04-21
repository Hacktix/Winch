using HarmonyLib;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using Winch.Util;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(LocalizedStringDatabase))]
    [HarmonyPatch("ProcessUntranslatedText")]
    internal class LocalizationRedirectPatcher
    {
        public static bool Prefix(ref string __result, string key, long keyId, TableReference tableReference, StringTable table, Locale locale)
        {
            string localeCode = locale.Identifier.Code;
            string localized = LocalizationUtil.GetLocalizedString(localeCode, key);
            if (localized == null)
                return true;

            __result = localized;
            return false;
        }
    }
}
