using LibarySystem.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurera unika index
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.MemberId)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Email)
                .IsUnique();

            // Konfigurera relationer
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);  // Förhindra att böcker tas bort om de har lån

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Member)
                .WithMany(m => m.Loans)
                .HasForeignKey(l => l.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data med STATISKA värden (inga DateTime.Now!)
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    ISBN = "978-91-0-012345-6",
                    Title = "Sagan om ringen",
                    Author = "J.R.R. Tolkien",
                    PublishedYear = 1954,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    ISBN = "978-91-0-012346-3",
                    Title = "Hobbiten",
                    Author = "J.R.R. Tolkien",
                    PublishedYear = 1937,
                    IsAvailable = false
                },
                new Book
                {
                    Id = 3,
                    ISBN = "978-91-0-012347-0",
                    Title = "Harry Potter och de vises sten",
                    Author = "J.K. Rowling",
                    PublishedYear = 1997,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 4,
                    ISBN = "978-91-0-012348-7",
                    Title = "1984",
                    Author = "George Orwell",
                    PublishedYear = 1949,
                    IsAvailable = true
                },
                new Book
                {
                    Id = 5,
                    ISBN = "978-91-0-012349-4",
                    Title = "Bröderna Lejonhjärta",
                    Author = "Astrid Lindgren",
                    PublishedYear = 1973,
                    IsAvailable = true
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 1,
                    MemberId = "M001",
                    Name = "Anna Andersson",
                    Email = "anna.andersson@email.com",
                    MemberSince = new DateTime(2024, 1, 15)  // STATISKT datum
                },
                new Member
                {
                    Id = 2,
                    MemberId = "M002",
                    Name = "Bengt Svensson",
                    Email = "bengt.svensson@email.com",
                    MemberSince = new DateTime(2024, 2, 20)  // STATISKT datum
                },
                new Member
                {
                    Id = 3,
                    MemberId = "M003",
                    Name = "Cecilia Johansson",
                    Email = "cecilia.johansson@email.com",
                    MemberSince = new DateTime(2024, 3, 10)  // STATISKT datum
                }
            );

            // Seed data för lån (om du vill ha några testlån)
            modelBuilder.Entity<Loan>().HasData(
                new Loan
                {
                    Id = 1,
                    BookId = 2,  // Hobbiten
                    MemberId = 1,  // Anna Andersson
                    LoanDate = new DateTime(2024, 3, 1),
                    DueDate = new DateTime(2024, 3, 15),
                    ReturnDate = null  // Inte returnerad än
                }
            );
        }
    }
}
