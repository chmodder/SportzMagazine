﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;


namespace SportzMagazine.Helpers
{
    public class NumberConverter : IValueConverter
    {
        #region Implementation of IValueConverter


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int number = (int)value;
            if (number == 0)
            {
                return null;//return null so the TextBox will display none.
            }
            else
            {
                return number;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }


        #endregion
    }
}
