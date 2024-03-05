using System;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni
{
    public class ValidateCurrentDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputDate = value as DateTime? ?? new DateTime();


            if (inputDate.Date >= DateTime.Today)
            {
                return true;
            }
            else
            {
                ErrorMessage = "La data dev'essere maggiore o uguale alla data odierna.";
                return false;
            }
        }
    }
}