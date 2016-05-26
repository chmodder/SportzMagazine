using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportzMagazine.Models;

namespace SportzMagazine.Helpers
{
    public class SimulatedPaymentSystem
    {


        /// <summary>
        /// this is will only check if the card does not expire within the next week, since it is only a simulation of a real payment system
        /// </summary>
        /// <param name="cardHolderName"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cardType"></param>
        /// <param name="expirationDate"></param>
        /// <returns>bool</returns>
        internal static bool VerifyCreditCard(string cardHolderName, string cardNumber, string cardType, DateTime expirationDate)
        {
            
            bool isValid = false;
            
            //Creates a variable to hold the date a week from now, which will be compared to the cards expirationDate
            DateTime oneWeekFromNow = DateTime.Today.AddDays(7);
            
            //sets the return value to true if card does not expire before a week from today
            if (!(expirationDate < oneWeekFromNow))
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
