using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Utils.Validations
{
    class AgeValidation : IValidation {
        public string Message { get; set; } = "This field should contain only positive integer values between 1 and 120.";

        public bool Validate(object value) {
            if (value is string text && !string.IsNullOrWhiteSpace(text)) {
                if (int.TryParse(text, out int number)) {
                    return number >= 1 && number <= 120;
                }
            }
            return false;
        }
    }
}
