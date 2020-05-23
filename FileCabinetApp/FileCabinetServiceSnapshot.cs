using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace FileCabinetApp
{
    /// <summary>
    /// Implement Snapshot logic.
    /// </summary>
    public class FileCabinetServiceSnapshot
    {
        private FileCabinetRecord[] fileCabinetRecords;
        private FileCabinetRecordCsvWriter csvWriter;
        private FileCabinetRecordXmlWriter xmlWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetServiceSnapshot"/> class.
        /// </summary>
        /// <param name="fileCabinetRecords">Array of all records.</param>
        public FileCabinetServiceSnapshot(FileCabinetRecord[] fileCabinetRecords)
        {
            FileCabinetRecord[] clonedRecords = new FileCabinetRecord[fileCabinetRecords.Length];

            for (int i = 0; i < fileCabinetRecords.Length; i++)
            {
                clonedRecords[i] = (FileCabinetRecord)fileCabinetRecords[i].Clone();
            }

            this.fileCabinetRecords = clonedRecords;
        }

        /// <summary>
        /// Method calls FileCabinet Record CsvWriter. Write and send there record.
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

        /// <summary>
        /// Method calls FileCabinet Record XmlWriter. Write and send there record.
        /// </summary>
        /// <param name="streamWriter">Opened.</param>
        public void SaveToXml(StreamWriter streamWriter)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            XmlWriter xmlWriter = XmlWriter.Create(streamWriter, settings);
            this.xmlWriter = new FileCabinetRecordXmlWriter(xmlWriter);

            xmlWriter.WriteStartElement("records");

            foreach (var item in this.fileCabinetRecords)
            {
                this.xmlWriter.Write(item);
            }

            xmlWriter.WriteEndElement();

            xmlWriter.Close();
        }
    }
}
