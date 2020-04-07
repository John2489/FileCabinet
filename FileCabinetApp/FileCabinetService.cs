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

            return record.Id;
        }

        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, short succsesfullDeals, decimal additionCoefficient, char manegerClass)
        {
            FileCabinetRecord editingElement = this.list.Where<FileCabinetRecord>(t => t.Id == id).FirstOrDefault();
            if (editingElement == null)
            {
                throw new ArgumentException($"Editing element whis id {id} does not exist.");
            }

            editingElement.FirstName = firstName;
            editingElement.LastName = lastName;
            editingElement.DateOfBirth = dateOfBirth;
            editingElement.SuccsesfullDeals = succsesfullDeals;
            editingElement.AdditionCoefficient = additionCoefficient;
            editingElement.ManegerClass = manegerClass;
        }

        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            return this.list.Where<FileCabinetRecord>(t => t.FirstName.ToUpper(CultureInfo.CreateSpecificCulture("en-US")) == firstName).ToArray();
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
