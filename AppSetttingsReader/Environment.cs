using System;
using System.ComponentModel;
using System.Configuration;

namespace ConfigReader
{
    public static class Environment
    {
        public static TValue GetValue<TValue>(string variable)
        {
            var value = System.Environment.GetEnvironmentVariable(variable);

            if (value is null)
                throw new ConfigurationErrorsException($"Configuração não existente: {variable}");

            return (TValue)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFromInvariantString(value);
        }

        public static TValue GetValueOrDefault<TValue>(string key, TValue defaultValue)
        {
            try
            {
                return GetValue<TValue>(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static bool TryGetValue<TValue>(string key, out TValue value)
        {
            try
            {
                value = GetValue<TValue>(key);
                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
        }
    }
}
