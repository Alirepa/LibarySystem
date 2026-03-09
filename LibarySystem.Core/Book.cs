using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class Book : ISearchable
    {
        [Key]
        public int Id { get; set; }  // Nytt ID för databasen

        [Required]
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Author { get; set; } = string.Empty;

        [Range(1000, 2100)]
        public int PublishedYear { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation property för relation till Loan
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        // Behåll din konstruktor för manuell skapelse
        public Book() { } // EF kräver en parameterlös konstruktor

        public Book(string isbn, string title, string author, int publishedYear)
        {
            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            PublishedYear = publishedYear;
            IsAvailable = true;
        }

        public string GetInfo()
        {
            return $"ISBN: {ISBN}, Title: {Title}, Author: {Author}, Year: {PublishedYear}, Available: {IsAvailable}";
        }

        public bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return false;

            var searchLower = searchTerm.ToLower();
            return Title.ToLower().Contains(searchLower) ||
                   Author.ToLower().Contains(searchLower) ||
                   ISBN.ToLower().Contains(searchLower);
        }
    }
}