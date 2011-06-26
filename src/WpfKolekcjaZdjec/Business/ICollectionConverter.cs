using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace WpfKolekcjaZdjec.View
{
    /// <summary>
    /// Collection converter.
    /// </summary>
    [ValueConversion(typeof(ICollection), typeof(String))]
    public class ICollectionConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICollection c = value as ICollection;

            if (c != null && c.Count > 0)
            {
                StringBuilder s = new StringBuilder();
                foreach (object obj in c)
                {
                    s.Append(obj.ToString());
                    s.Append(", ");
                }

                if (s.Length > 1)
                {
                    return s.ToString().Substring(0, s.Length - 2);
                }

                return s.ToString();
            }

            return "(Empty)";
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}