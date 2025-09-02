using System;
using ClientRecordsDesktopApp.Utils.Validations;

namespace ClientRecordsDesktopAppUnitTests.Validations {
    public class AgeValidationTests {
        private readonly AgeValidation _validator;

        public AgeValidationTests() {
            _validator = new AgeValidation();
        }

        [Fact]
        public void Message_ShouldHaveDefaultValue() {
            // Act & Assert
            Assert.Equal("This field should contain only positive values between 1 and 120.", _validator.Message);
        }

        [Fact]
        public void Message_ShouldBeSettable() {
            // Arrange
            var customMessage = "Custom age validation message";

            // Act
            _validator.Message = customMessage;

            // Assert
            Assert.Equal(customMessage, _validator.Message);
        }

        [Theory]
        [InlineData("1", true)]
        [InlineData("18", true)]
        [InlineData("25", true)]
        [InlineData("65", true)]
        [InlineData("120", true)]
        public void Validate_WithValidAgeStrings_ShouldReturnTrue(string ageString, bool expected) {
            // Act
            var result = _validator.Validate(ageString);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0", false)]
        [InlineData("-1", false)]
        [InlineData("-10", false)]
        [InlineData("121", false)]
        [InlineData("150", false)]
        [InlineData("999", false)]
        public void Validate_WithInvalidAgeRanges_ShouldReturnFalse(string ageString, bool expected) {
            // Act
            var result = _validator.Validate(ageString);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abc", false)]
        [InlineData("25.5", false)]
        [InlineData("twenty", false)]
        [InlineData("25abc", false)]
        [InlineData("abc25", false)]
        [InlineData("2.5", false)]
        [InlineData("25 years", false)]
        [InlineData("1.0", false)]
        public void Validate_WithNonNumericStrings_ShouldReturnFalse(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("\t", false)]
        [InlineData("\n", false)]
        [InlineData(" \t \n ", false)]
        public void Validate_WithNullOrWhitespaceStrings_ShouldReturnFalse(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(25, false)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(121, false)]
        public void Validate_WithIntegerValues_ShouldReturnFalse(int input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(25.5, false)]
        [InlineData(0.0, false)]
        [InlineData(-1.5, false)]
        public void Validate_WithDoubleValues_ShouldReturnFalse(double input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Validate_WithCustomObject_ShouldReturnFalse() {
            // Arrange
            var customObject = new { Age = 25 };

            // Act
            var result = _validator.Validate(customObject);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(" 25 ", true)]
        [InlineData("  1  ", true)]
        [InlineData(" 120 ", true)]
        [InlineData("   50   ", true)]
        public void Validate_WithValidAgeStringsWithSpaces_ShouldReturnTrue(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00001", true)]
        [InlineData("0025", true)]
        [InlineData("000120", true)]
        public void Validate_WithLeadingZeros_ShouldParseCorrectly(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("+25", true)]
        [InlineData("+1", true)]
        [InlineData("+120", true)]
        [InlineData("+0", false)]
        [InlineData("+121", false)]
        public void Validate_WithPositiveSign_ShouldHandleCorrectly(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Validate_WithMaxInt_ShouldReturnFalse() {
            // Arrange
            var maxIntString = int.MaxValue.ToString();

            // Act
            var result = _validator.Validate(maxIntString);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WithMinInt_ShouldReturnFalse() {
            // Arrange
            var minIntString = int.MinValue.ToString();

            // Act
            var result = _validator.Validate(minIntString);

            // Assert
            Assert.False(result);
        }
    }
}
