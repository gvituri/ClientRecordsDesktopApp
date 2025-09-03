using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Utils.Validations
{
    public class AgeValidation : IValidation {
        public string Message { get; set; } = "Este campo só pode conter inteiros positivos entre 1 e 120.";

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
