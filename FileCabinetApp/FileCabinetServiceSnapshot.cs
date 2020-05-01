using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Implement Snapshot logic.
    /// </summary>
    public class FileCabinetServiceSnapshot
    {
        private FileCabinetRecord[] fileCabinetRecords;
        private FileCabinetRecordCsvWriter csvWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetServiceSnapshot"/> class.
        /// </summary>
        /// <param name="fileCabinetRecords">Array of all records.</param>
        public FileCabinetServiceSnapshot(FileCabinetRecord[] fileCabinetRecords)
        {
            this.fileCabinetRecords = fileCabinetRecords;
        }

        /// <summary>
        /// Method calls FileCabinet Record CsvWriter.Write and send there record.
        /// </summary>
        /// <param name="streamWriter">Opened.</param>
        public void SaveToCsv(StreamWriter streamWriter)
        {
            this.csvWriter = new FileCabinetRecordCsvWriter(streamWriter);

            foreach (var item in this.fileCabinetRecords)
            {
                this.csvWriter.Write(item);
            }
        }
    }
}
