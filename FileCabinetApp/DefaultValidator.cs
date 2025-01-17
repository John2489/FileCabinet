﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Concrete class for default validation.
    /// </summary>
    public class DefaultValidator : IRecordValidator
    {
        /// <summary>
        /// Method implements default validation.
        /// </summary>
        /// <param name="param">Instance that describes all information of record.</param>
        /// <returns>Return true if validation is okey or false with ArgumentExeption if validation is not okey.</returns>
        public bool ValidatePatameters(ObjectParametrsForCreateAndEditRecord param)
        {
            if (param.FirstName == null || param.FirstName.Length < 2 || param.FirstName.Length > 60 || param.FirstName.Contains(' ', StringComparison.CurrentCulture))
            {
                return false;
                throw new ArgumentException("parametr \"firstName\" is not correct.");
            }

            if (param.LastName == null || param.LastName.Length < 2 || param.LastName.Length > 60 || param.LastName.Contains(' ', StringComparison.CurrentCulture))
            {
                return false;
                throw new ArgumentException("parametr \"lastName\" is not correct.");
            }

            if (param.DateOfBirth == null || param.DateOfBirth < new DateTime(1950, 1, 1) || param.DateOfBirth >= DateTime.Now)
            {
                return false;
                throw new ArgumentException("parametr \"dateOfBirth\" is not correct.");
            }

            if (param.SuccsesfullDeals <= 0)
            {
                return false;
                throw new ArgumentException("parametr \"succsesfullDeals\" is not correct.");
            }

            if (param.AdditionCoefficient <= 0)
            {
                return false;
                throw new ArgumentException("parametr \"additionCoefficient\" is not correct.");
            }

            if (!char.IsLetter(param.ManagerClass))
            {
                return false;
                throw new ArgumentException("parametr \"manegerClass\" is not correct.");
            }

            return true;
        }
    }
}
