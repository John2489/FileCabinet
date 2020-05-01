using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// This class is abstract and includes and implements the behavior of the File Cabinet service.
    /// </summary>
    public class FileCabinetService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthNameDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();
        private readonly IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetService"/> class.
        /// </summary>
        /// <param name="validator">This interface describes validation method.</param>
        public FileCabinetService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Method creates new record.
        /// </summary>
        /// <param name="param">Instance that describe all information of record.</param>
        /// <returns>Identification number of record.</returns>
        public int CreateRecord(ObjectParametrsForCreateAndEditRecord param)
        {
            if (!this.validator.ValidatePatameters(param))
            {
                return 0;
            }

            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = param.FirstName,
                LastName = param.LastName,
                DateOfBirth = param.DateOfBirth,
                SuccsesfullDeals = param.SuccsesfullDeals,
                AdditionCoefficient = param.AdditionCoefficient,
                ManagerClass = param.ManagerClass,
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
        /// <param name="param">Instance that describe all information of record.</param>
        public void EditRecord(int id, ObjectParametrsForCreateAndEditRecord param)
        {
            if (!this.validator.ValidatePatameters(param))
            {
                return;
            }

            FileCabinetRecord editingElement = this.list.Where<FileCabinetRecord>(t => t.Id == id).FirstOrDefault();
            if (editingElement == null || param == null)
            {
                throw new ArgumentException($"Editing element whis id {id} does not exist.");
            }

            string oldFirstName = editingElement.FirstName;
            string oldLastName = editingElement.LastName;
            DateTime oldDateOfBirth = new DateTime(editingElement.DateOfBirth.Year, editingElement.DateOfBirth.Month, editingElement.DateOfBirth.Day);

            editingElement.FirstName = param.FirstName;
            editingElement.LastName = param.LastName;
            editingElement.DateOfBirth = param.DateOfBirth;
            editingElement.SuccsesfullDeals = param.SuccsesfullDeals;
            editingElement.AdditionCoefficient = param.AdditionCoefficient;
            editingElement.ManagerClass = param.ManagerClass;

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
        /// <returns>Returns Read Only Collection of records where first name in each one record the same.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            if (firstName != null)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(this.firstNameDictionary[firstName.ToUpper(CultureInfo.CurrentCulture)].ToList<FileCabinetRecord>());
            }

            return null;
        }

        /// <summary>
        /// Method findes records by last name.
        /// </summary>
        /// <param name="lastName">Last name of person in record.</param>
        /// <returns>Returns Read Only Collection of records where last name in each one record the same.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            if (lastName != null)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(this.lastNameDictionary[lastName.ToUpper(CultureInfo.CurrentCulture)].ToList<FileCabinetRecord>());
            }

            return null;
        }

        /// <summary>
        /// Method findes records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth of person in record.</param>
        /// <returns>Returns Read Only Collection of records where date of birth in each one record the same.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirthName(DateTime dateOfBirth)
        {
            if (dateOfBirth != null)
            {
                return new ReadOnlyCollection<FileCabinetRecord>(this.dateOfBirthNameDictionary[dateOfBirth].ToList<FileCabinetRecord>());
            }

            return null;
        }

        /// <summary>
        /// This method is to gets all records.
        /// </summary>
        /// <returns>Returns Read Only Collection of all records in array view.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.list);
        }

        /// <summary>
        /// Method is to count records.
        /// </summary>
        /// <returns>Returns quantity of records.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }

        /// <summary>
        /// Create new instans of FileCabinetServiceSnapshot.
        /// </summary>
        /// <returns>New instans of FileCabinetServiceSnapshot.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            FileCabinetRecord[] files = this.list.ToArray();
            FileCabinetRecord[] filesCopied = (FileCabinetRecord[])files.Clone();
            return new FileCabinetServiceSnapshot(filesCopied);
        }
    }
}