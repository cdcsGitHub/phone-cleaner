using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TrainingExercise1
{
    /// <summary>
    /// This class is used to clean up customer 
    /// phone numbers, so that the only numbers 
    /// displayed in the end fit the assignment
    /// requirements.
    /// </summary>
    public class DataProcess
    {
        /// <summary>
        /// This regex statement is used to define which
        /// characters can be allowed in the phone number.
        /// </summary>
        private static Regex digitsOnly = new Regex(@"[^\d]");

        /// <summary>
        /// Returns a phone number in string format that only contains 
        /// positive integer characters.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string CleanPhone(string phone)
        {
            return digitsOnly.Replace(phone, "");
        }

        /// <summary>
        /// Returns a phone number in string formate if all requirements are met, 
        /// or a blank string if a requirements is not met.
        /// </summary>
        /// <param name="customerPhoneNumber"></param>
        /// <returns></returns>
        public string PhoneNumberClean(string customerPhoneNumber)
        {
            string invalid_phone = "1234567890";
            string invalid_phone1 = "0123456789";
            int counter = 0;
            int i = 1;

            //Deletes non-numeric characters with Regex\\
            customerPhoneNumber = CleanPhone(customerPhoneNumber);
            //Checks to see if first digit is 0\\
            
            //Checks to make sure the phone number is exactly ten digits\\
            if (!customerPhoneNumber.Length.Equals(10))
                return String.Empty;
            else
            {
                if (customerPhoneNumber[0].Equals('0'))
                    return String.Empty;
                else
                {
                    while (customerPhoneNumber[i] == customerPhoneNumber[i - 1])
                    {
                        if (i < 3)
                        {
                            counter++;
                            i++;
                            if (counter == 2)
                                return String.Empty;
                        }
                        else
                        {
                            counter = 0;
                            i = 1;
                            if (i < 7)
                            {
                                counter++;
                                i++;
                                if (counter == 6)
                                    return String.Empty;
                            }
                        }
                    }
                    if ((customerPhoneNumber.Equals(invalid_phone)) || (customerPhoneNumber.Equals(invalid_phone1)))
                        return String.Empty;
                }
            }
                return customerPhoneNumber;
        }

        /// <summary>
        /// Returns a combined string of phone number and 
        /// extension number if all requirements are met.
        /// A blank string is returned if a requirement is 
        /// not met.
        /// </summary>
        /// <param name="businessPhone"></param>
        /// <param name="extNumber"></param>
        /// <returns></returns>
        public string extCreator(string businessPhone, string extNumber)
        {
            if (String.IsNullOrEmpty(businessPhone))
                return String.Empty;
            else
            {
                //Remove non-numerics with Regex\\
                extNumber = CleanPhone(extNumber);
                //Checks to see if there is a 0 in the ext number\\
                //Blanks out column if yes\\
                if (extNumber.Contains("0"))
                    return String.Empty;
                else
                //Concatenates the Business Phone column and Ext Column\\
                {
                    if (!String.IsNullOrEmpty(extNumber) && !String.IsNullOrEmpty(businessPhone))
                    {
                        extNumber = extNumber.Insert(0, "  x");
                        extNumber = businessPhone.Insert(businessPhone.Length, extNumber);
                    }
                }
            }
            return extNumber;
        }
    }
}
