using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Utils.Validations
{
    class SqlFriendlyStringValidation : IValidation {
        public string Message { get; set; } = "This field should not contain ', \",\\.";

        public bool Validate(object value) {
            if (value is string text && !string.IsNullOrWhiteSpace(text)) {
                return !(text.Contains("'") || text.Contains("\"") || text.Contains("\\"));
            }

            return false;
        }
    }
}
