using System;
using System.ComponentModel;
using System.Configuration;

namespace ConfigReader
{
    public static class Environment
    {
        /// <summary>
        /// Gets the value of an Environment variable.
        /// </summary>
        /// <param name="key">Configuration key.</param>
        /// <returns>Configuration value.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Thrown when no configuration was found with the given key.
        /// </exception>
        public static TValue GetValue<TValue>(string key)
        {
            var value = System.Environment.GetEnvironmentVariable(key);

            if (value is null)
                throw new ConfigurationErrorsException($"Configuration not found: {key}");

            return (TValue)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFromInvariantString(value);
        }

        /// <summary>
        /// Gets the value of an Environment variable.
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
        /// Gets the value of an Environment variable.
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
