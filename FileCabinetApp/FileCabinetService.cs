using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// This class includes and implements the behavior of the File Cabinet service.
    /// </summary>
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthNameDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        /// <summary>
        /// Method creates new record.
        /// </summary>
        /// <param name="firstName">First name of person in record.</param>
        /// <param name="lastName">Last name of person in record.</param>
        /// <param name="dateOfBirth">Date of birth of person in record.</param>
        /// <param name="succsesfullDeals">Quantity of succsesfull deals of person in record.</param>
        /// <param name="additionCoefficient">Addition coefficient to salary of person in record.</param>
        /// <param name="managerClass">Class of manager in record.</param>
        /// <returns>Identification number of record.</returns>
        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, short succsesfullDeals, decimal additionCoefficient, char managerClass)
        {
            if (firstName == null || firstName.Length < 2 || firstName.Length > 60 || firstName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException("parametr \"firstName\" is not correct.");
            }

            if (lastName == null || lastName.Length < 2 || lastName.Length > 60 || lastName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException("parametr \"lastName\" is not correct.");
            }

            if (dateOfBirth == null || dateOfBirth < new DateTime(1950, 1, 1) || dateOfBirth >= DateTime.Now)
            {
                throw new ArgumentException("parametr \"dateOfBirth\" is not correct.");
            }

            if (succsesfullDeals <= 0)
            {
                throw new ArgumentException("parametr \"succsesfullDeals\" is not correct.");
            }

            if (additionCoefficient <= 0)
            {
                throw new ArgumentException("parametr \"additionCoefficient\" is not correct.");
            }

            if (!char.IsLetter(managerClass))
            {
                throw new ArgumentException("parametr \"manegerClass\" is not correct.");
            }

            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                SuccsesfullDeals = succsesfullDeals,
                AdditionCoefficient = additionCoefficient,
                ManagerClass = managerClass,
            };

            this.list.Add(record);

            if (this.firstNameDictionary.ContainsKey(record.FirstName.ToUpper(CultureInfo.CurrentCulture)))
            {
                this.firstNameDictionary[record.FirstName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.FirstName == record.FirstName).ToList();
            }
            else
            {
                this.firstNameDictionary.Add(record.FirstName.ToUpper(CultureInfo.CurrentCulture), this.list.Where<FileCabinetRecord>(t => t.FirstName == record.FirstName).ToList());
            }

            if (this.lastNameDictionary.ContainsKey(record.LastName.ToUpper(CultureInfo.CurrentCulture)))
            {
                this.lastNameDictionary[record.LastName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.LastName == record.LastName).ToList();
            }
            else
            {
                this.lastNameDictionary.Add(record.LastName.ToUpper(CultureInfo.CurrentCulture), this.list.Where<FileCabinetRecord>(t => t.LastName == record.LastName).ToList());
            }

            if (this.dateOfBirthNameDictionary.ContainsKey(record.DateOfBirth))
            {
                this.dateOfBirthNameDictionary[record.DateOfBirth] = this.list.Where<FileCabinetRecord>(t => t.DateOfBirth == record.DateOfBirth).ToList();
            }
            else
            {
                this.dateOfBirthNameDictionary.Add(record.DateOfBirth, this.list.Where<FileCabinetRecord>(t => t.DateOfBirth == record.DateOfBirth).ToList());
            }

            return record.Id;
        }

        /// <summary>
        ///  Method edited an existing record.
        /// </summary>
        /// <param name="id">Identification number of record which editing.</param>
        /// <param name="firstName">First name of person in record.</param>
        /// <param name="lastName">Last name of person in record.</param>
        /// <param name="dateOfBirth">Date of birth of person in record.</param>
        /// <param name="succsesfullDeals">Quantity of succsesfull deals of person in record.</param>
        /// <param name="additionCoefficient">Addition coefficient to salary of person in record.</param>
        /// <param name="managerClass">Class of maneger in record.</param>
        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, short succsesfullDeals, decimal additionCoefficient, char managerClass)
        {
            FileCabinetRecord editingElement = this.list.Where<FileCabinetRecord>(t => t.Id == id).FirstOrDefault();
            if (editingElement == null)
            {
                throw new ArgumentException($"Editing element whis id {id} does not exist.");
            }

            string oldFirstName = editingElement.FirstName;
            string oldLastName = editingElement.LastName;
            DateTime oldDateOfBirth = new DateTime(editingElement.DateOfBirth.Year, editingElement.DateOfBirth.Month, editingElement.DateOfBirth.Day);

            editingElement.FirstName = firstName;
            editingElement.LastName = lastName;
            editingElement.DateOfBirth = dateOfBirth;
            editingElement.SuccsesfullDeals = succsesfullDeals;
            editingElement.AdditionCoefficient = additionCoefficient;
            editingElement.ManagerClass = managerClass;

            if (this.firstNameDictionary.ContainsKey(oldFirstName.ToUpper(CultureInfo.CurrentCulture)))
            {
                    this.firstNameDictionary[oldFirstName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.FirstName == oldFirstName).ToList();
            }

            if (this.lastNameDictionary.ContainsKey(oldLastName.ToUpper(CultureInfo.CurrentCulture)))
            {
                this.lastNameDictionary[oldLastName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.LastName == oldLastName).ToList();
            }

            if (this.dateOfBirthNameDictionary.ContainsKey(oldDateOfBirth))
            {
                this.dateOfBirthNameDictionary[oldDateOfBirth] = this.list.Where<FileCabinetRecord>(t => t.DateOfBirth == oldDateOfBirth).ToList();
            }

            if (editingElement.FirstName != null && this.firstNameDictionary.ContainsKey(editingElement.FirstName.ToUpper(CultureInfo.CurrentCulture)))
            {
                this.firstNameDictionary[editingElement.FirstName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.FirstName == editingElement.FirstName).ToList();
            }
            else
            {
                this.firstNameDictionary.Add(editingElement.FirstName.ToUpper(CultureInfo.CurrentCulture), this.list.Where<FileCabinetRecord>(t => t.FirstName == editingElement.FirstName).ToList());
            }

            if (editingElement.LastName != null && this.lastNameDictionary.ContainsKey(editingElement.LastName.ToUpper(CultureInfo.CurrentCulture)))
            {
                this.lastNameDictionary[editingElement.LastName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.LastName == editingElement.LastName).ToList();
            }
            else
            {
                this.lastNameDictionary.Add(editingElement.LastName.ToUpper(CultureInfo.CurrentCulture), this.list.Where<FileCabinetRecord>(t => t.LastName == editingElement.LastName).ToList());
            }

            if (editingElement.DateOfBirth != null && this.dateOfBirthNameDictionary.ContainsKey(editingElement.DateOfBirth))
            {
                this.dateOfBirthNameDictionary[editingElement.DateOfBirth] = this.list.Where<FileCabinetRecord>(t => t.DateOfBirth == editingElement.DateOfBirth).ToList();
            }
            else
            {
                this.dateOfBirthNameDictionary.Add(editingElement.DateOfBirth, this.list.Where<FileCabinetRecord>(t => t.DateOfBirth == editingElement.DateOfBirth).ToList());
            }
        }

        /// <summary>
        /// Method findes records by first name.
        /// </summary>
        /// <param name="firstName">First name of person in record.</param>
        /// <returns>Returns array of records where first name in each one record the same.</returns>
        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (firstName != null)
            {
                return this.firstNameDictionary[firstName.ToUpper(CultureInfo.CurrentCulture)].ToArray();
            }

            return null;

            // return this.list.Where<FileCabinetRecord>(t => t.FirstName.ToUpper(CultureInfo.CreateSpecificCulture("en-US")) == firstName).ToArray();
        }

        /// <summary>
        /// Method findes records by last name.
        /// </summary>
        /// <param name="lastName">Last name of person in record.</param>
        /// <returns>Returns array of records where last name in each one record the same.</returns>
        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            if (lastName != null)
            {
                return this.lastNameDictionary[lastName.ToUpper(CultureInfo.CurrentCulture)].ToArray();
            }

            return null;

            // return this.list.Where<FileCabinetRecord>(t => t.LastName.ToUpper(CultureInfo.CreateSpecificCulture("en-US")) == lastName).ToArray();
        }

        /// <summary>
        /// Method findes records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth of person in record.</param>
        /// <returns>Returns array of records where date of birth in each one record the same.</returns>
        public FileCabinetRecord[] FindByDateOfBirthName(DateTime dateOfBirth)
        {
            if (dateOfBirth != null)
            {
                return this.dateOfBirthNameDictionary[dateOfBirth].ToArray();
            }

            return null;

            // return this.list.Where(t => t.DateOfBirth == dateOfBirth).ToArray();
        }

        /// <summary>
        /// This method is to gets all records.
        /// </summary>
        /// <returns>Returns all records in array view.</returns>
        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        /// <summary>
        /// Method is to count records.
        /// </summary>
        /// <returns>Returns quantity of records.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
