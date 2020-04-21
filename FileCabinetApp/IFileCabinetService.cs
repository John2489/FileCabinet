using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// This interface describes base methods for Records.
    /// </summary>
    public interface IFileCabinetService
    {
        /// <summary>
        /// Method creates new record.
        /// </summary>
        /// <param name="param">Instance that describe all information of record.</param>
        /// <returns>Identification number of record.</returns>
        public int CreateRecord(ObjectParametrsForCreateAndEditRecord param);

        /// <summary>
        ///  Method edited an existing record.
        /// </summary>
        /// <param name="id">Identification number of record which editing.</param>
        /// <param name="param">Instance that describe all information of record.</param>
        public void EditRecord(int id, ObjectParametrsForCreateAndEditRecord param);

        /// <summary>
        /// Method findes records by first name.
        /// </summary>
        /// <param name="firstName">First name of person in record.</param>
        /// <returns>Returns Read Only Collection of records where first name in each one record the same.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName);

        /// <summary>
        /// Method findes records by last name.
        /// </summary>
        /// <param name="lastName">Last name of person in record.</param>
        /// <returns>Returns Read Only Collection of records where last name in each one record the same.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName);

        /// <summary>
        /// Method findes records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth of person in record.</param>
        /// <returns>Returns Read Only Collection of records where date of birth in each one record the same.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirthName(DateTime dateOfBirth);

        /// <summary>
        /// This method is to gets all records.
        /// </summary>
        /// <returns>Returns Read Only Collection of all records in array view.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords();

        /// <summary>
        /// Method is to count records.
        /// </summary>
        /// <returns>Returns quantity of records.</returns>
        public int GetStat();
    }
}
