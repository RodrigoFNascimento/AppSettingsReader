using System;
using System.ComponentModel;
using System.Configuration;

namespace AppSetttingsReader
{
    /// <summary>
    /// Ferramentas usadas para obter configurações do AppSettings.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Obtém o valor de uma configuração do AppSettings.
        /// </summary>
        /// <param name="key">Identificador da configuração.</param>
        /// <returns>Valor da configuração.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Lançada quando a configuração está em formato inválido ou não existe.
        /// </exception>
        public static string GetValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(value))
                throw new ConfigurationErrorsException($"Configuração inválida ou não existente: {key}");

            return value;
        }

        /// <summary>
        /// Obtém o valor de uma configuração do AppSettings.
        /// </summary>
        /// <typeparam name="TValue">Tipo para o qual o valor deve ser convertido.</typeparam>
        /// <param name="key">Identificador da configuração.</param>
        /// <returns>Valor da configuração convertido.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Lançada quando a configuração está em formato inválido ou não existe.
        /// </exception>
        public static TValue GetValue<TValue>(string key)
        {
            var value = GetValue(key);

            return (TValue)TypeDescriptor.GetConverter(typeof(TValue)).ConvertFromInvariantString(value);
        }

        /// <summary>
        /// Obtém o valor de uma configuração do AppSettings ou valor default.
        /// </summary>
        /// <typeparam name="TValue">Tipo para o qual o valor deve ser convertido.</typeparam>
        /// <param name="key">Identificador da configuração.</param>
        /// <param name="defaultValue">Valor retornado caso não seja possível obter valor do AppSettings.</param>
        /// <returns>Valor da configuração convertido.</returns>
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
        /// Obtém o valor de uma configuração do AppSettings.
        /// </summary>
        /// <remarks>
        /// Se o valor não for convertido com sucesso, 
        /// o valor retornado de <paramref name="value"></paramref> 
        /// será o default do tipo <typeparamref name="TValue"></typeparamref>.
        /// </remarks>
        /// <typeparam name="TValue">Tipo para o qual o valor deve ser convertido.</typeparam>
        /// <param name="key">Identificador da configuração.</param>
        /// <param name="value">Valor convertido da configuração.</param>
        /// <returns>true se <paramref name="key"></paramref> for convertido com sucesso; do contrário, false.</returns>
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
