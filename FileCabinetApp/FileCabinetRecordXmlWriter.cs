using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

namespace FileCabinetApp
{
    /// <summary>
    /// Instance of this class implement export to .XML file.
    /// </summary>
    public class FileCabinetRecordXmlWriter
    {
        private XmlWriter xmlWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordXmlWriter"/> class.
        /// </summary>
        /// <param name="xmlWriter">Opened XmlWriter stream instance.</param>
        public FileCabinetRecordXmlWriter(XmlWriter xmlWriter)
        {
            this.xmlWriter = xmlWriter;
        }

        /// <summary>
        /// Write info aboute one record in opened XmlWriter stream instance.
        /// </summary>
        /// <param name="fileCabinetRecord">FileCabinetRecord object.</param>
        public void Write(FileCabinetRecord fileCabinetRecord)
        {
            this.xmlWriter.WriteStartElement("record");

            this.xmlWriter.WriteAttributeString("id", fileCabinetRecord.Id.ToString(CultureInfo.CurrentCulture));
            this.xmlWriter.WriteStartElement("name");
            this.xmlWriter.WriteAttributeString("firstname", fileCabinetRecord.FirstName);
            this.xmlWriter.WriteAttributeString("lastname", fileCabinetRecord.LastName);
            this.xmlWriter.WriteEndElement();

            this.xmlWriter.WriteElementString("dateOfBirth", fileCabinetRecord.DateOfBirth.ToString("MM/dd/yyyy", CultureInfo.CurrentCulture));
            this.xmlWriter.WriteElementString("succsesfullDeals", fileCabinetRecord.SuccsesfullDeals.ToString(CultureInfo.CurrentCulture));
            this.xmlWriter.WriteElementString("additionCoefficient", fileCabinetRecord.AdditionCoefficient.ToString(CultureInfo.CurrentCulture));
            this.xmlWriter.WriteElementString("managerClass", fileCabinetRecord.ManagerClass.ToString(CultureInfo.CurrentCulture));

            this.xmlWriter.WriteEndElement();
        }
    }
}
