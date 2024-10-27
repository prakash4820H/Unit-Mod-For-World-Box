
using NCMS.Utils;
using System.Collections.Generic;
using System.Reflection;

namespace Unit
{
    public static class LocalizationUtility
    {
        public static void AddLocalization(string key, string value)
        {
            Localization.Add(key, value);
        }
        public static T GetField<T>(object obj, string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) return default(T); // Field not found
            return (T)field.GetValue(obj);
        }

        public static void AddOrSet(string key, string value)
        {
            Localization.Add(key, value);
        }

        public static void addTraitToLocalizedLibrary(string id, string description)
        {
            string language = GetField<string>(LocalizedTextManager.instance, "language");
            Dictionary<string, string> localizedText = GetField<Dictionary<string, string>>(LocalizedTextManager.instance, "localizedText");
            localizedText.Add("trait_" + id, id);
            localizedText.Add("trait_" + id + "_info", description);
        }
    }
}
