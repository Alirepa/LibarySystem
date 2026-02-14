using LibarySystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Tests
{
    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenDueDateIsInFuture()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act & Assert
            Assert.False(loan.IsOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenDueDateHasPassed()
        {
            // Testa med ett förfallet lån

            //arange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person","test@test.com");
            var pastDate = DateTime.Now.AddDays(-7);
            var loan = new Loan (book, member, pastDate, pastDate.AddDays(1));

            //act and assert
            Assert.True(loan.IsOverdue);
        }

        [Fact]
        public void IsReturned_ShouldReturnTrue_WhenReturnDateIsSet()
        {
            // Testa att IsReturned fungerar korrekt
            //arnage
            var book = new Book("123","Test","Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            //act
            loan.ReturnBook();

            //asert
            Assert.True(loan.IsReturned);
            Assert.True(book.IsAvailable);

        }
        [Fact]
        public void Loan_ShouldMakeBookUnavailable()
        {
            //arange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");

            //act
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            //assert
            Assert.False(book.IsAvailable);
        }
    }
}
