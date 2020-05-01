using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.ConvertersAndVilidators
{
    /// <summary>
    /// Class for converters for user's input.
    /// </summary>
    public static class Converters
    {
        /// <summary>
        /// Check if user pass not an empty line or not a not a null.
        /// </summary>
        /// <param name="userLine">User's input.</param>
        /// <returns>Tuple, where is item1 is true if validation passed, item2 is message about unsuccessful validation, item3 is convertated user's input.</returns>
        public static Tuple<bool, string, string> StringConverter(string userLine)
        {
            bool item1 = false;
            string item2 = string.Empty;
            string item3 = string.Empty;
            if (userLine != null && userLine != " ")
            {
                item1 = true;
                item3 = userLine;
            }
            else
            {
                item2 = "You entered empty line";
            }

            return new Tuple<bool, string, string>(item1, item2, item3);
        }

        /// <summary>
        /// Check if user's date of birth input is correct or not.
        /// </summary>
        /// <param name="userLine">User's input.</param>
        /// <returns>Tuple, where is item1 is true if validation passed, item2 is message about unsuccessful validation, item3 is convertated user's input.</returns>
        public static Tuple<bool, string, DateTime> DateOfBirthConverter(string userLine)
        {
            bool item1 = false;
            string item2 = string.Empty;
            DateTime item3 = DateTime.Now;

            string[] dayMonthHear;
            int[] dateSepareted = new int[3];
            dayMonthHear = userLine.Split('/');

            if (dayMonthHear.Length != 3)
            {
                item2 = "Date is not full";
                return new Tuple<bool, string, DateTime>(item1, item2, item3);
            }

            if (int.TryParse(dayMonthHear[0], out dateSepareted[0]) &&
                int.TryParse(dayMonthHear[1], out dateSepareted[1]) &&
                int.TryParse(dayMonthHear[2], out dateSepareted[2]))
            {
                try
                {
                    item3 = new DateTime(dateSepareted[2], dateSepareted[0], dateSepareted[1]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    item2 = "Invalid Date format";
                    return new Tuple<bool, string, DateTime>(item1, item2, item3);
                }

                item1 = true;
                return new Tuple<bool, string, DateTime>(item1, item2, item3);
            }
            else
            {
                item2 = "Invalid Date vormat";
                return new Tuple<bool, string, DateTime>(item1, item2, item3);
            }
        }

        /// <summary>
        /// Check if user's quantity of succsesfullDeals input is correct or not.
        /// </summary>
        /// <param name="userLine">User's input.</param>
        /// <returns>Tuple, where is item1 is true if validation passed, item2 is message about unsuccessful validation, item3 is convertated user's input.</returns>
        public static Tuple<bool, string, short> SuccsesfullDealsConverter(string userLine)
        {
            bool item1 = false;
            string item2 = string.Empty;
            short item3 = 0;

            if (short.TryParse(userLine, out item3))
            {
                item1 = true;
                return new Tuple<bool, string, short>(item1, item2, item3);
            }
            else
            {
                item2 = "You entered not a number";
                return new Tuple<bool, string, short>(item1, item2, item3);
            }
        }

        /// <summary>
        /// Check if user's quantity of succsesfullDeals input is correct or not.
        /// </summary>
        /// <param name="userLine">User's input.</param>
        /// <returns>Tuple, where is item1 is true if validation passed, item2 is message about unsuccessful validation, item3 is convertated user's input.</returns>
        public static Tuple<bool, string, decimal> AdditionCoefficientConverter(string userLine)
        {
            bool item1 = false;
            string item2 = string.Empty;
            decimal item3 = 0;

            if (decimal.TryParse(userLine, out item3))
            {
                item1 = true;
                return new Tuple<bool, string, decimal>(item1, item2, item3);
            }
            else
            {
                item2 = "You entered not a number or the coefficient is very small";
                return new Tuple<bool, string, decimal>(item1, item2, item3);
            }
        }

        /// <summary>
        /// Check if user's ManegerClass input is correct or not.
        /// </summary>
        /// <param name="userLine">User's input.</param>
        /// <returns>Tuple, where is item1 is true if validation passed, item2 is message about unsuccessful validation, item3 is convertated user's input.</returns>
        public static Tuple<bool, string, char> ManegerClassConverter(string userLine)
        {
            bool item1 = false;
            string item2 = string.Empty;
            char item3 = 'X';

            if (userLine.Length == 1 && char.IsLetter(userLine[0]))
            {
                item1 = true;
                item3 = userLine[0];
                return new Tuple<bool, string, char>(item1, item2, item3);
            }
            else
            {
                item2 = "You entered not a character";
                return new Tuple<bool, string, char>(item1, item2, item3);
            }
        }
    }
}