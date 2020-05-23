using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// This class describes entity of record.
    /// </summary>
    public class FileCabinetRecord : ICloneable
    {
        /// <summary>
        /// Gets or sets identification number of record.
        /// </summary>
        /// <value>
        /// Identification number of record.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets first name of record.
        /// </summary>
        /// <value>
        /// First name of record.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name of record.
        /// </summary>
        /// <value>
        /// Last name of record.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets date of birth of record.
        /// </summary>
        /// <value>
        /// Date of birth of person in record.
        /// </value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets quantity of succsesfull deals of record.
        /// </summary>
        /// <value>
        /// Quantity of succsesfull deals of record.
        /// </value>
        public short SuccsesfullDeals { get; set; }

        /// <summary>
        /// Gets or sets addition coefficient to salary of record.
        /// </summary>
        /// <value>
        /// Addition coefficient to salary of record.
        /// </value>
        public decimal AdditionCoefficient { get; set; }

        /// <summary>
        /// Gets or sets manager class of record.
        /// </summary>
        /// <value>
        /// Manager class of record.
        /// </value>
        public char ManagerClass { get; set; }

        /// <summary>
        /// Make deep clone of FileCabinetRecord.
        /// </summary>
        /// <returns>Clone of FileCabinetRecord.</returns>
        public object Clone()
        {
            var temp = new FileCabinetRecord();
            temp.Id = this.Id;
            temp.FirstName = this.FirstName;
            temp.LastName = this.LastName;
            temp.DateOfBirth = this.DateOfBirth;
            temp.SuccsesfullDeals = this.SuccsesfullDeals;
            temp.AdditionCoefficient = this.AdditionCoefficient;
            temp.ManagerClass = this.ManagerClass;
            return temp;
        }
    }
}
