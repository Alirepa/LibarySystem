using LibarySystem.Core;

namespace LibrarySystem.Console
{
    class Program
    {
        private static Library _library = new Library();

        static void Main(string[] args)
        {
            SeedData(); // Lägg till testdata

            while (true)
            {
                ShowMenu();
                var choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllBooks();
                        break;
                    case "2":
                        SearchBooks();
                        break;
                    case "3":
                        BorrowBook();
                        break;
                    case "4":
                        ReturnBook();
                        break;
                    case "5":
                        ShowAllMembers();
                        break;
                    case "6":
                        ShowStatistics();
                        break;
                    case "0":
                        System.Console.WriteLine("Välkommen åter!");
                        return;
                    default:
                        System.Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

                System.Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                System.Console.ReadKey();
                System.Console.Clear();
            }
        }

        static void ShowMenu()
        {
            System.Console.WriteLine("=== Bibliotekssystem ===\n");
            System.Console.WriteLine("1. Visa alla böcker");
            System.Console.WriteLine("2. Sök bok");
            System.Console.WriteLine("3. Låna bok");
            System.Console.WriteLine("4. Returnera bok");
            System.Console.WriteLine("5. Visa medlemmar");
            System.Console.WriteLine("6. Statistik");
            System.Console.WriteLine("0. Avsluta");
            System.Console.Write("\nVälj: ");
        }

        static void ShowAllBooks()
        {
            System.Console.WriteLine("\n=== Alla böcker ===\n");
            var books = _library.SortBooksByTitle();

            foreach (var book in books)
            {
                var status = book.IsAvailable ? "Tillgänglig" : "Utlånad";
                System.Console.WriteLine($"\"{book.Title}\" av {book.Author} ({book.PublishedYear}) - {status}");
            }
        }

        static void SearchBooks()
        {
            System.Console.Write("\nSökterm: ");
            var searchTerm = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                System.Console.WriteLine("Ogiltig sökterm.");
                return;
            }

            var results = _library.SearchBooks(searchTerm).ToList();

            System.Console.WriteLine($"\nSökresultat ({results.Count} träffar):\n");

            int index = 1;
            foreach (var book in results)
            {
                var status = book.IsAvailable ? "Tillgänglig" : "Utlånad";
                System.Console.WriteLine($"{index}. \"{book.Title}\" av {book.Author} ({book.PublishedYear}) - {status}");
                index++;
            }
        }

        static void BorrowBook()
        {
            System.Console.Write("\nAnge ISBN: ");
            var isbn = System.Console.ReadLine();

            System.Console.Write("Ange medlems-ID: ");
            var memberId = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn) || string.IsNullOrWhiteSpace(memberId))
            {
                System.Console.WriteLine("Ogiltig input.");
                return;
            }

            var loan = _library.BorrowBook(isbn, memberId);

            if (loan == null)
            {
                System.Console.WriteLine("Kunde inte låna boken. Kontrollera ISBN och medlems-ID.");
                return;
            }

            System.Console.WriteLine($"\nBoken \"{loan.Book.Title}\" har lånats ut till {loan.Member.Name}.");
            System.Console.WriteLine($"Återlämningsdatum: {loan.DueDate:yyyy-MM-dd}");
        }

        static void ReturnBook()
        {
            System.Console.Write("\nAnge ISBN på boken som ska returneras: ");
            var isbn = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                System.Console.WriteLine("Ogiltig input.");
                return;
            }

            var success = _library.ReturnBook(isbn);

            if (success)
            {
                System.Console.WriteLine("Boken har returnerats.");
            }
            else
            {
                System.Console.WriteLine("Kunde inte returnera boken. Kontrollera ISBN.");
            }
        }

        static void ShowAllMembers()
        {
            System.Console.WriteLine("\n=== Alla medlemmar ===\n");

            // OBS: Du behöver lägga till en metod i Library för att hämta alla medlemmar
            // Här är en tillfällig lösning tills du lägger till den metoden
            System.Console.WriteLine("Funktionen kommer snart...");
        }

        static void ShowStatistics()
        {
            System.Console.WriteLine("\n=== Statistik ===\n");
            System.Console.WriteLine(_library.GetLibraryStatistics());
        }

        static void SeedData()
        {
            // Lägg till testdata
            var book1 = new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954);
            var book2 = new Book("978-91-0-012346-3", "Hobbiten", "J.R.R. Tolkien", 1937);
            var book3 = new Book("978-91-0-012347-0", "Harry Potter", "J.K. Rowling", 1997);

            _library.AddBook(book1);
            _library.AddBook(book2);
            _library.AddBook(book3);

            var member1 = new Member("M001", "Anna Andersson", "anna@email.com");
            var member2 = new Member("M002", "Bengt Svensson", "bengt@email.com");

            _library.RegisterMember(member1);
            _library.RegisterMember(member2);

            // Låna ut en bok för att visa "Utlånad" status
            _library.BorrowBook("978-91-0-012346-3", "M001"); // Låna ut Hobbiten
        }
    }
}
