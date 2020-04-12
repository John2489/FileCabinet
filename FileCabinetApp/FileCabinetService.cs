using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, short succsesfullDeals, decimal additionCoefficient, char manegerClass)
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

            if (!char.IsLetter(manegerClass))
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
                ManegerClass = manegerClass,
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

            return record.Id;
        }

        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, short succsesfullDeals, decimal additionCoefficient, char manegerClass)
        {
            FileCabinetRecord editingElement = this.list.Where<FileCabinetRecord>(t => t.Id == id).FirstOrDefault();
            if (editingElement == null)
            {
                throw new ArgumentException($"Editing element whis id {id} does not exist.");
            }

            string oldFirstName = editingElement.FirstName;

            editingElement.FirstName = firstName;
            editingElement.LastName = lastName;
            editingElement.DateOfBirth = dateOfBirth;
            editingElement.SuccsesfullDeals = succsesfullDeals;
            editingElement.AdditionCoefficient = additionCoefficient;
            editingElement.ManegerClass = manegerClass;

            if (this.firstNameDictionary.ContainsKey(oldFirstName.ToUpper(CultureInfo.CurrentCulture)))
            {
                    this.firstNameDictionary[oldFirstName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.FirstName == oldFirstName).ToList();
            }

            if (editingElement.FirstName != null && this.firstNameDictionary.ContainsKey(editingElement.FirstName.ToUpper(CultureInfo.CurrentCulture)))
            {
                this.firstNameDictionary[editingElement.FirstName.ToUpper(CultureInfo.CurrentCulture)] = this.list.Where<FileCabinetRecord>(t => t.FirstName == editingElement.FirstName).ToList();
            }
            else
            {
                this.firstNameDictionary.Add(editingElement.FirstName.ToUpper(CultureInfo.CurrentCulture), this.list.Where<FileCabinetRecord>(t => t.FirstName == editingElement.FirstName).ToList());
            }
        }

        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (firstName != null)
            {
                return this.firstNameDictionary[firstName.ToUpper(CultureInfo.CurrentCulture)].ToArray();
            }

            return null;

            // return this.list.Where<FileCabinetRecord>(t => t.FirstName.ToUpper(CultureInfo.CreateSpecificCulture("en-US")) == firstName).ToArray();
        }

        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            return this.list.Where<FileCabinetRecord>(t => t.LastName.ToUpper(CultureInfo.CreateSpecificCulture("en-US")) == lastName).ToArray();
        }

        public FileCabinetRecord[] FindByDateOfBirthName(DateTime dateOfBirth)
        {

            return this.list.Where(t => t.DateOfBirth == dateOfBirth).ToArray();
        }

        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
