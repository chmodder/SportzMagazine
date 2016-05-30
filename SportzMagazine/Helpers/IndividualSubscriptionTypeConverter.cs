using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using SportzMagazine.Models;

namespace SportzMagazine.Helpers
{
    public class IndividualSubscriptionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            
            IndividualSubscription returnValue = new IndividualSubscription();

            try
            {
                if (value.GetType() == typeof(IndividualSubscription))
                {
                    returnValue = value as IndividualSubscription;
                }
                
            }
            catch (Exception ex)
            {
                returnValue = null;
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Subscription returnValue = new Subscription();

            try
            {
                returnValue = value as Subscription;
                
            }
            catch (Exception ex)
            {
                returnValue = null;
            }
            return returnValue;
        }
    }
}
