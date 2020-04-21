using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Custom validation for File Cabinet Servise.
    /// </summary>
    public class FileCabinetCustomService : FileCabinetService
    {
        /// <summary>
        /// Method for return instance of concrete custom validation.
        /// </summary>
        /// <returns>Instance of concrete custom validation.</returns>
        public override IRecordValidator CreateValidator()
        {
            return new CustomValidator();
        }
    }
}
