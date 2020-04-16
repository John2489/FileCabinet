using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Instance of this class serve like a parameter to Edit and Create methods.
    /// </summary>
    public class ObjectParametrsForCreateAndEditRecord
    {
        /// <summary>
        /// Gets first name of record.
        /// </summary>
        /// <value>
        /// First name of record.
        /// </value>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets last name of record.
        /// </summary>
        /// <value>
        /// Last name of record.
        /// </value>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets date of birth of record.
        /// </summary>
        /// <value>
        /// Date of birth of person in record.
        /// </value>
        public DateTime DateOfBirth { get; private set; }

        /// <summary>
        /// Gets quantity of succsesfull deals of record.
        /// </summary>
        /// <value>
        /// Quantity of succsesfull deals of record.
        /// </value>
        public short SuccsesfullDeals { get; private set; }

        /// <summary>
        /// Gets addition coefficient to salary of record.
        /// </summary>
        /// <value>
        /// Addition coefficient to salary of record.
        /// </value>
        public decimal AdditionCoefficient { get; private set; }

        /// <summary>
        /// Gets manager class of record.
        /// </summary>
        /// <value>
        /// Manager class of record.
        /// </value>
        public char ManagerClass { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectParametrsForCreateAndEditRecord"/> class.
        /// </summary>
        /// <param name="firstName">First name of record.</param>
        /// <param name="lastName">Last name of record.</param>
        /// <param name="dateOfBirth">Date of birth of person in record.</param>
        /// <param name="succsesfullDeals">Quantity of succsesfull deals of record.</param>
        /// <param name="additionCoefficient">Addition coefficient to salary of record.</param>
        /// <param name="managerClass">Manager class of record.</param>
        public ObjectParametrsForCreateAndEditRecord(string firstName, string lastName, DateTime dateOfBirth, short succsesfullDeals, decimal additionCoefficient, char managerClass)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.SuccsesfullDeals = succsesfullDeals;
            this.AdditionCoefficient = additionCoefficient;
            this.ManagerClass = managerClass;
        }
    }
}
