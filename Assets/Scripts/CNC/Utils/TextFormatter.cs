using System;
using System.Globalization;

namespace CNC.Utils
{
    public static class TextFormatter
    {
        private const string DefaultFormat = "0.000";
        private static readonly IFormatProvider FormatProvider = CultureInfo.InvariantCulture;

        /// <summary>
        /// Форматирует double в строку с форматом "0.000"
        /// </summary>
        public static string Format(double value)
        {
            return value.ToString(DefaultFormat, FormatProvider);
        }

        /// <summary>
        /// Форматирует float в строку с форматом "0.000"
        /// </summary>
        public static string Format(float value)
        {
            return value.ToString(DefaultFormat, FormatProvider);
        }

        /// <summary>
        /// Форматирует int в строку с форматом "0.000" (например 5 -> "5.000")
        /// </summary>
        public static string Format(int value)
        {
            return value.ToString(DefaultFormat, FormatProvider);
        }

        /// <summary>
        /// Форматирует decimal в строку с форматом "0.000"
        /// </summary>
        public static string Format(decimal value)
        {
            return value.ToString(DefaultFormat, FormatProvider);
        }

        /// <summary>
        /// Парсит строку в double с использованием InvariantCulture
        /// </summary>
        public static double ParseDouble(string value)
        {
            return double.Parse(value, FormatProvider);
        }

        /// <summary>
        /// Безопасный парсинг строки в double с возвратом значения по умолчанию
        /// </summary>
        public static double ParseDoubleOrDefault(string value, double defaultValue = 0.0)
        {
            return double.TryParse(value, NumberStyles.Float, FormatProvider, out double result) 
                ? result 
                : defaultValue;
        }

        /// <summary>
        /// Парсит строку в float с использованием InvariantCulture
        /// </summary>
        public static float ParseFloat(string value)
        {
            return float.Parse(value, FormatProvider);
        }

        /// <summary>
        /// Безопасный парсинг строки в float с возвратом значения по умолчанию
        /// </summary>
        public static float ParseFloatOrDefault(string value, float defaultValue = 0f)
        {
            return float.TryParse(value, NumberStyles.Float, FormatProvider, out float result) 
                ? result 
                : defaultValue;
        }
        
        public static bool TryParseFloat(string value, out float result)
        {
            return float.TryParse(value, NumberStyles.Float, FormatProvider, out result);
        }

        /// <summary>
        /// Парсит строки в int с использованием InvariantCulture
        /// </summary>
        public static int ParseInt(string value)
        {
            return int.Parse(value, FormatProvider);
        }

        /// <summary>
        /// Безопасный парсинг строки в int с возвратом значения по умолчанию
        /// </summary>
        public static int ParseIntOrDefault(string value, int defaultValue = 0)
        {
            return int.TryParse(value, NumberStyles.Integer, FormatProvider, out int result) 
                ? result 
                : defaultValue;
        }

        /// <summary>
        /// Форматирует объект в строку. Поддерживает double, float, int, decimal.
        /// Для остальных типов возвращает ToString() с InvariantCulture
        /// </summary>
        public static string Format(object value)
        {
            return value switch
            {
                double d => d.ToString(DefaultFormat, FormatProvider),
                float f => f.ToString(DefaultFormat, FormatProvider),
                int i => i.ToString(DefaultFormat, FormatProvider),
                decimal m => m.ToString(DefaultFormat, FormatProvider),
                _ => Convert.ToString(value, FormatProvider) ?? string.Empty
            };
        }
    }
}