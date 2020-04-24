using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// This interface describes validation method.
    /// </summary>
    public interface IRecordValidator
    {
        /// <summary>
        /// This method describes validation. Must be redefined.
        /// </summary>
        /// <param name="param">Instance that describes all information of record.</param>
        /// <returns>Return true if validation is okey or false with ArgumentExeption if validation is not okey.</returns>
        public bool ValidatePatameters(ObjectParametrsForCreateAndEditRecord param);
    }
}
