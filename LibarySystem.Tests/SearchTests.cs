using LibarySystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Tests
{
    public class SearchTests
    {
        [Theory]
        [InlineData("Tolkien", true)]
        [InlineData("tolkien", true)]  // Case-insensitive
        [InlineData("Rowling", false)]
        [InlineData("Sagan", true)]    // Matchar titel
        [InlineData("978-0-395", true)] // Matchar ISBN
        [InlineData("", false)]         // Tom sökterm
        [InlineData("   ", false)]      // Whitespace
        public void Book_Matches_ShouldFindCorrectly(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("978-0-395-19395-7", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
