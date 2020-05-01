using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Instance of this class implement export to .CSV file.
    /// </summary>
    public class FileCabinetRecordCsvWriter
    {
        private TextWriter textWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordCsvWriter"/> class.
        /// </summary>
        /// <param name="textWriter">Opened TextWriter stream instance.</param>
        public FileCabinetRecordCsvWriter(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        /// <summary>
        /// Write info aboute one record in opened TextWriter stream instance.
        /// </summary>
        /// <param name="fileCabinetRecord">FileCabinetRecord object.</param>
        public void Write(FileCabinetRecord fileCabinetRecord)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"{fileCabinetRecord.Id}, ");
            builder.Append($"{fileCabinetRecord.FirstName}, ");
            builder.Append($"{fileCabinetRecord.LastName}, ");
            builder.Append($"{fileCabinetRecord.DateOfBirth.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}, ");
            builder.Append($"{fileCabinetRecord.SuccsesfullDeals}, ");
            builder.Append($"{fileCabinetRecord.AdditionCoefficient}, ");
            builder.Append($"{fileCabinetRecord.ManagerClass.ToString(CultureInfo.InvariantCulture)},");

            this.textWriter.WriteLine(builder.ToString());
        }
    }
}
