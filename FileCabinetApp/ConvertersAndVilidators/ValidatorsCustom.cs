using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp.ConvertersAndVilidators
{
    /// <summary>
    /// Custom validation class for user's input.
    /// </summary>
    public static class ValidatorsCustom
    {
        /// <summary>
        /// Validation for FirstName.
        /// </summary>
        /// <param name="userLine">User's input.</param>
        /// <returns>Tuple, where item 1 is true if validation passed, item2 is message about unsuccessful validation.</returns>
        public static Tuple<bool, string> FirstNameValidator(string userLine)
        {
            bool item1 = false;
            string item2 = string.Empty;

            if (userLine.Length < 2 || userLine.Length > 60)
            {
                item1 = false;
                item2 = "First name must be more than 2 and less than 60 characters";
                foreach (var item in userLine)
                {
                    if (!char.IsLetter(item))
                    {
                        item2 += " and should not contains not a letter";
                        break;
                    }
                }
            }
            else
            {
                item1 = true;
            }

            return new Tuple<bool, string>(item1, item2);
        }

        /// <summary>
        /// Validation for LastName.
        /// </summary>
        /// <param name="userLine">User's input.</param>
        /// <returns>Tuple, where item 1 is true if validation passed, item2 is message about unsuccessful validation.</returns>
        public static Tuple<bool, string> LastNameValidator(string userLine)
        {
            bool item1 = false;
            string item2 = string.Empty;

            if (userLine.Length < 2 || userLine.Length > 60 || userLine == null)
            {
                item1 = false;
                item2 = "Last name must be more than 2 and less than 60 characters";
                foreach (var item in userLine)
                {
                    if (!char.IsLetter(item))
                    {
                        item2 += " and should not contains not a letter";
                        break;
                    }
                }
            }
            else
            {
                item1 = true;
            }

            return new Tuple<bool, string>(item1, item2);
        }

        /// <summary>
        /// Validation for FirstName.
        /// </summary>
        /// <param name="userDate">User's input.</param>
        /// <returns>Tuple, where item 1 is true if validation passed, item2 is message about unsuccessful validation.</returns>
        public static Tuple<bool, string> DateOfBirthValidator(DateTime userDate)
        {
            bool item1 = false;
            string item2 = string.Empty;
            DateTime minDate = new DateTime(1940, 1, 1);

            if (userDate < minDate || userDate > DateTime.Now)
            {
                item2 = "Date of birth must be not older than 01/01/1940 and not younger than now";
                return new Tuple<bool, string>(item1, item2);
            }

            item1 = true;
            return new Tuple<bool, string>(item1, item2);
        }

        /// <summary>
        /// Validation for SuccsesfullDeals.
        /// </summary>
        /// <param name="userQuantity">User's input.</param>
        /// <returns>Tuple, where item 1 is true if validation passed, item2 is message about unsuccessful validation.</returns>
        public static Tuple<bool, string> SuccsesfullDealsValidator(short userQuantity)
        {
            bool item1 = false;
            string item2 = string.Empty;

            if (userQuantity >= 0 || userQuantity >= 5_000)
            {
                item1 = true;
                return new Tuple<bool, string>(item1, item2);
            }
            else
            {
                item2 = "Quantity of succsesfull deals must be more than zero and not more than 5000";
                return new Tuple<bool, string>(item1, item2);
            }
        }

        /// <summary>
        /// Validation for AdditionCoefficient.
        /// </summary>
        /// <param name="userCoefficient">User's input.</param>
        /// <returns>Tuple, where item 1 is true if validation passed, item2 is message about unsuccessful validation.</returns>
        public static Tuple<bool, string> AdditionCoefficientValidator(decimal userCoefficient)
        {
            bool item1 = false;
            string item2 = string.Empty;

            if (userCoefficient >= 0 || userCoefficient <= 2)
            {
                item1 = true;
                return new Tuple<bool, string>(item1, item2);
            }
            else
            {
                item2 = "Addition сoefficient must be between zero and 2";
                return new Tuple<bool, string>(item1, item2);
            }
        }

        /// <summary>
        /// Validation for ManegerClass.
        /// </summary>
        /// <param name="userClass">User's input.</param>
        /// <returns>Tuple, where item 1 is true if validation passed, item2 is message about unsuccessful validation.</returns>
        public static Tuple<bool, string> ManegerClassValidator(char userClass)
        {
            bool item1 = false;
            string item2 = string.Empty;

            if (char.IsLetter(userClass))
            {
                item1 = true;
                return new Tuple<bool, string>(item1, item2);
            }
            else
            {
                item2 = "Maneger Class must be a letter";
                return new Tuple<bool, string>(item1, item2);
            }
        }
    }
}
