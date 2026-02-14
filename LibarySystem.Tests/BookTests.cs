using LibarySystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

            // Assert
            Assert.Equal("978-91-0-012345-6", book.ISBN);
            Assert.Equal("Testbok", book.Title);
            Assert.Equal("Testförfattare", book.Author);
            Assert.Equal(2024, book.PublishedYear);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void IsAvailable_ShouldBeTrueForNewBook()
        {
            // Arrange
            var book = new Book("123", "New Book", "Author", 2023);

            // Act & Assert
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void GetInfo_ShouldReturnFormattedString()
        {
            //Testa att nya böcker är tillgängliga
            // Arrange
            var book = new Book("978-123-456", "C# Programming", "John Doe", 2023);

            // Act
            var result = book.GetInfo();

            // Assert
            Assert.Contains("ISBN: 978-123-456", result);
            Assert.Contains("Title: C# Programming", result);
            Assert.Contains("Author: John Doe", result);
            Assert.Contains("Year: 2023", result);
        }

        [Fact]
        public void ISBN_ShouldBeReadOnly()
        {
            // Testa att GetInfo() returnerar korrekt format
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);

            // Act & Assert 
            Assert.Equal("123", book.ISBN);
        }
    }
}
