using System;
using System.Reflection;

namespace Unit
{
    public static class ReflectionUtility
    {

        public static T GetField<T>(object obj, string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) return default; // Field not found
            return (T)field.GetValue(obj);
        }

        public static T GetFieldValue<T>(object obj, string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) throw new Exception($"Field '{fieldName}' not found in type '{obj.GetType()}'");
            return (T)field.GetValue(obj);
        }

        public static void SetFieldValue<T>(object obj, string fieldName, T value)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null) throw new Exception($"Field '{fieldName}' not found in type '{obj.GetType()}'");
            field.SetValue(obj, value);
        }

        public static T CallMethod<T>(object obj, string methodName, params object[] args)
        {
            MethodInfo method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) throw new Exception($"Method '{methodName}' not found in type '{obj.GetType()}'");
            return (T)method.Invoke(obj, args);
        }

        public static void CallMethod(object obj, string methodName, params object[] args)
        {
            MethodInfo method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) throw new Exception($"Method '{methodName}' not found in type '{obj.GetType()}'");
            method.Invoke(obj, args);
        }

        public static T GetStaticFieldValue<T>(Type type, string fieldName)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            if (field == null) throw new Exception($"Field '{fieldName}' not found in type '{type}'");
            return (T)field.GetValue(null);
        }
    }
}
