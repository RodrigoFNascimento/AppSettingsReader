using System;
using System.ComponentModel;
using System.Configuration;

namespace AppSetttingsReader
{
    public static class AppSettings
    {
        /// <summary>
        /// Gets a value from AppSettings.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Configuration value.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Thrown when no configuration was found with the given key.
        /// </exception>
        public static string GetValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (value is null)
                throw new ConfigurationErrorsException($"Configuration not found: {key}");

            return value;
        }

        /// <summary>
        /// Gets a value from AppSettings.
        /// </summary>
        /// <typeparam name="TValue">Output type.</typeparam>
        /// <param name="key">Configuration key.</param>
        /// <returns>Configuration value of type <typeparamref name="TValue"/>.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the value cannot be converted to <typeparamref name="TValue"/>.
        /// </exception>
        /// <exception cref="ConfigurationErrorsException">
        /// Thrown when no configuration was found with the given key.
        /// </exception>
        public static TValue GetValue<TValue>(string key)
        {
            var value = GetValue(key);

            return (TValue)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFromInvariantString(value);
        }

        /// <summary>
        /// Gets a value from AppSettings.
        /// </summary>
        /// <typeparam name="TValue">Output type.</typeparam>
        /// <param name="key">Configuration key.</param>
        /// <param name="defaultValue">
        /// Return value when <paramref name="key"/> is not found
        /// or cannot be converted to <typeparamref name="TValue"/>.
        /// </param>
        /// <returns>Configuration value of type <typeparamref name="TValue"/>.</returns>
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

        /// <summary>
        /// Gets a value from AppSettings.
        /// </summary>
        /// <remarks>
        /// If the value cannot be successfully converted,
        /// the output value will be the default of type <typeparamref name="TValue"/>.
        /// </remarks>
        /// <typeparam name="TValue">Output type.</typeparam>
        /// <param name="key">Configuration key.</param>
        /// <param name="value">Configuration value of type <typeparamref name="TValue"/>.</param>
        /// <returns>true if <paramref name="value"/> was converted successfully; otherwise, false.</returns>
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
