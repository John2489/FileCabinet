using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Default validation for File Cabinet Servise.
    /// </summary>
    public class FileCabinetDefaultService : FileCabinetService
    {
        /// <summary>
        /// Method for return instance of concrete default validation.
        /// </summary>
        /// <returns>Instance of concrete default validation.</returns>
        public override IRecordValidator CreateValidator()
        {
            return new DefaultValidator();
        }
    }
}
