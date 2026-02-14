using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class Book : ISearchable
    {
        public string ISBN { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string isbn, string title, string author, int publishedYear) 
        {
            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException( nameof(author));
            PublishedYear = publishedYear;
            IsAvailable = true;
        }

        public string GetInfo()
        {
            return $"ISBN: {ISBN}, Title: {Title}, Author: {Author}, Year: {PublishedYear}, Available: {IsAvailable}";
        }

        public bool Matches (string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))return false;

            var searchLower = searchTerm.ToLower();
            return Title.ToLower().Contains(searchLower) || 
                Author.ToLower().Contains(searchLower) || 
                ISBN.ToLower().Contains(searchLower);
        }
    }
}
