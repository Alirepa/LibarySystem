using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Tests
{
    using LibarySystem.Core;
    using System;
    using Xunit;

    namespace LibrarySystem.Tests
    {
        public class LibraryStatisticsTests
        {
            [Fact]
            public void GetTotalBooks_ShouldReturnCorrectCount()
            {
                // Arrange
                var library = new Library();
                library.AddBook(new Book("1", "Book 1", "Author 1", 2020));
                library.AddBook(new Book("2", "Book 2", "Author 2", 2021));
                library.AddBook(new Book("3", "Book 3", "Author 3", 2022));

                // Act
                var result = library.GetTotalBooks();

                // Assert
                Assert.Equal(3, result);
            }

            [Fact]
            public void GetBorrowedBooksCount_ShouldReturnCorrectCount()
            {
                // Arrange
                var library = new Library();
                var book1 = new Book("1", "Book 1", "Author 1", 2020);
                var book2 = new Book("2", "Book 2", "Author 2", 2021);
                var member = new Member("M1", "John Doe", "john@test.com");

                library.AddBook(book1);
                library.AddBook(book2);
                library.RegisterMember(member);

                // Act
                library.BorrowBook("1", "M1");
                var result = library.GetBorrowedBooksCount();

                // Assert
                Assert.Equal(1, result);
            }

            [Fact]
            public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
            {
                // Arrange
                var library = new Library();
                var book1 = new Book("1", "Book 1", "Author 1", 2020);
                var book2 = new Book("2", "Book 2", "Author 2", 2021);
                var member1 = new Member("M1", "Active User", "active@test.com");
                var member2 = new Member("M2", "Inactive User", "inactive@test.com");

                library.AddBook(book1);
                library.AddBook(book2);
                library.RegisterMember(member1);
                library.RegisterMember(member2);

                // Act
                library.BorrowBook("1", "M1");
                library.BorrowBook("2", "M1"); // mem1 lånar 2 böcker

                var mostActive = library.GetMostActiveBorrower();

                // Assert
                Assert.NotNull(mostActive);
                Assert.Equal("M1", mostActive!.MemberId);
            }

            [Fact]
            public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
            {
                // Arrange
                var library = new Library();
                library.AddBook(new Book("3", "Charlie", "Author C", 2020));
                library.AddBook(new Book("1", "Alpha", "Author A", 2022));
                library.AddBook(new Book("2", "Bravo", "Author B", 2021));

                // Act
                var sorted = library.SortBooksByTitle();
                var titles = sorted.Select(b => b.Title).ToList();

                // Assert
                Assert.Equal("Alpha", titles[0]);
                Assert.Equal("Bravo", titles[1]);
                Assert.Equal("Charlie", titles[2]);
            }

            [Fact]
            public void SortBooksByPublishedYear_ShouldReturnChronologicalOrder()
            {
                // Arrange
                var library = new Library();
                library.AddBook(new Book("3", "Book 3", "Author C", 2022));
                library.AddBook(new Book("1", "Book 1", "Author A", 2020));
                library.AddBook(new Book("2", "Book 2", "Author B", 2021));

                // Act
                var sorted = library.SortBooksByPublishedYear();
                var years = sorted.Select(b => b.PublishedYear).ToList();

                // Assert
                Assert.Equal(2020, years[0]);
                Assert.Equal(2021, years[1]);
                Assert.Equal(2022, years[2]);
            }
        }
    }
}
