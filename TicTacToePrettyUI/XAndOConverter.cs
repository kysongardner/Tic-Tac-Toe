using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TicTacToePrettyUI
{
    public class XAndOConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ReturnedValue = System.Convert.ToInt32(value);

            if (ReturnedValue == 0)
                return 'O';
            else if (ReturnedValue == 1)
                return 'X';
            else
                return ' ';
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
