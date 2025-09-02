using ClientRecordsDesktopApp.Utils.Validations;

namespace ClientRecordsDesktopAppUnitTests.Validations {
    public class SqlFriendlyStringValidationTests {
        private readonly SqlFriendlyStringValidation _validator;

        public SqlFriendlyStringValidationTests() {
            _validator = new SqlFriendlyStringValidation();
        }

        [Fact]
        public void Message_ShouldHaveDefaultValue() {
            // Act & Assert
            Assert.Equal("This field should not contain ', \",\\.", _validator.Message);
        }

        [Fact]
        public void Message_ShouldBeSettable() {
            // Arrange
            var customMessage = "Custom SQL validation message";

            // Act
            _validator.Message = customMessage;

            // Assert
            Assert.Equal(customMessage, _validator.Message);
        }

        [Theory]
        [InlineData("John", true)]
        [InlineData("Jane Doe", true)]
        [InlineData("123 Main Street", true)]
        [InlineData("Simple text", true)]
        [InlineData("Numbers123", true)]
        [InlineData("Special@#$%^&*()characters", true)]
        [InlineData("Hyphen-text", true)]
        [InlineData("Under_score", true)]
        [InlineData("Dot.text", true)]
        [InlineData("Plus+text", true)]
        [InlineData("Equal=text", true)]
        public void Validate_WithValidStrings_ShouldReturnTrue(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("John's", false)]
        [InlineData("Can't", false)]
        [InlineData("It's working", false)]
        [InlineData("'", false)]
        [InlineData("''", false)]
        [InlineData("Text with ' apostrophe", false)]
        [InlineData("Multiple'apostrophes'here", false)]
        public void Validate_WithSingleQuotes_ShouldReturnFalse(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("John \"said\" hello", false)]
        [InlineData("\"", false)]
        [InlineData("\"\"", false)]
        [InlineData("Text with \" quote", false)]
        [InlineData("Multiple\"quotes\"here", false)]
        [InlineData("Say \"Hello World\"", false)]
        public void Validate_WithDoubleQuotes_ShouldReturnFalse(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Path\\to\\file", false)]
        [InlineData("\\", false)]
        [InlineData("\\\\", false)]
        [InlineData("Text with \\ backslash", false)]
        [InlineData("Multiple\\backslashes\\here", false)]
        [InlineData("C:\\Program Files\\App", false)]
        public void Validate_WithBackslashes_ShouldReturnFalse(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("John's \"house\" is at C:\\temp", false)]
        [InlineData("Mix'ed \"quo\\tes", false)]
        [InlineData("'\"\\", false)]
        [InlineData("Text' with\" all\\ bad chars", false)]
        public void Validate_WithMultipleForbiddenCharacters_ShouldReturnFalse(string input, bool expected) {
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
        [InlineData(123, false)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void Validate_WithIntegerValues_ShouldReturnFalse(int input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(25.5, false)]
        [InlineData(0.0, false)]
        public void Validate_WithDoubleValues_ShouldReturnFalse(double input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Validate_WithCustomObject_ShouldReturnFalse() {
            // Arrange
            var customObject = new { Name = "John" };

            // Act
            var result = _validator.Validate(customObject);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Normal text with spaces", true)]
        [InlineData("   Text with leading spaces", true)]
        [InlineData("Text with trailing spaces   ", true)]
        [InlineData("  Text with both  ", true)]
        public void Validate_WithValidTextIncludingSpaces_ShouldReturnTrue(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Unicode characters: áéíóú", true)]
        [InlineData("Chinese: 你好", true)]
        [InlineData("Emoji: 😀", true)]
        [InlineData("German: Müller", true)]
        [InlineData("French: Café", true)]
        public void Validate_WithUnicodeCharacters_ShouldReturnTrue(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Line\nbreak", true)]
        [InlineData("Tab\tcharacter", true)]
        [InlineData("Carriage\rreturn", true)]
        public void Validate_WithWhitespaceCharacters_ShouldReturnTrue(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Text with ' only", false)]
        [InlineData("Text with \" only", false)]
        [InlineData("Text with \\ only", false)]
        public void Validate_WithSingleForbiddenCharacter_ShouldReturnFalse(string input, bool expected) {
            // Act
            var result = _validator.Validate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Validate_WithVeryLongValidString_ShouldReturnTrue() {
            // Arrange
            var longString = new string('a', 10000);

            // Act
            var result = _validator.Validate(longString);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WithVeryLongStringContainingForbiddenChar_ShouldReturnFalse() {
            // Arrange
            var longString = new string('a', 5000) + "'" + new string('b', 5000);

            // Act
            var result = _validator.Validate(longString);

            // Assert
            Assert.False(result);
        }
    }
}
