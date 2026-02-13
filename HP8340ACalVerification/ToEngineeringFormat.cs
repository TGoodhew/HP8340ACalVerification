using System;

namespace HP8340ACalVerification
{
    /// <summary>
    /// Utility class to convert numeric values to engineering format with SI prefixes
    /// </summary>
    public static class ToEngineeringFormat
    {
        /// <summary>
        /// Convert a double value to engineering notation with appropriate SI prefix
        /// </summary>
        /// <param name="value">The numeric value to convert</param>
        /// <param name="unit">The unit symbol (e.g., "Hz", "V", "W")</param>
        /// <param name="decimalPlaces">Number of decimal places to display</param>
        /// <returns>Formatted string with SI prefix</returns>
        public static string Format(double value, string unit = "", int decimalPlaces = 3)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                return $"{value} {unit}";
            }

            if (value == 0)
            {
                return $"0 {unit}";
            }

            double absValue = Math.Abs(value);
            string sign = value < 0 ? "-" : "";

            string[] prefixes = { "y", "z", "a", "f", "p", "n", "µ", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y" };
            int index = 8; // Start at no prefix (10^0)

            if (absValue >= 1000)
            {
                while (absValue >= 1000 && index < prefixes.Length - 1)
                {
                    absValue /= 1000;
                    index++;
                }
            }
            else if (absValue < 1)
            {
                while (absValue < 1 && index > 0)
                {
                    absValue *= 1000;
                    index--;
                }
            }

            string formatString = $"{{0:F{decimalPlaces}}}";
            string valueStr = string.Format(formatString, absValue);
            
            // Only add space before unit if unit is not empty
            if (string.IsNullOrEmpty(unit))
            {
                return $"{sign}{valueStr}{prefixes[index]}";
            }
            else
            {
                return $"{sign}{valueStr} {prefixes[index]}{unit}";
            }
        }

        /// <summary>
        /// Convert a decimal value to engineering notation with appropriate SI prefix
        /// </summary>
        /// <param name="value">The numeric value to convert</param>
        /// <param name="unit">The unit symbol (e.g., "Hz", "V", "W")</param>
        /// <param name="decimalPlaces">Number of decimal places to display</param>
        /// <returns>Formatted string with SI prefix</returns>
        public static string Format(decimal value, string unit = "", int decimalPlaces = 3)
        {
            return Format((double)value, unit, decimalPlaces);
        }
    }
}
