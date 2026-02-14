# Bibliotekssystem

Ett konsolbaserat bibliotekssystem för hantering av böcker, medlemmar och utlåning.

## Kort beskrivning av lösningen

Systemet är byggt med objektorienterade principer i C# och använder **komposition** (Alternativ B) där ett huvudbibliotek (`Library`) innehåller tre hanteringsklasser:
- `BookCatalog` - hanterar böcker
- `MemberRegistry` - hanterar medlemmar
- `LoanManager` - hanterar utlåning

**Huvudfunktioner:**
- Skapa böcker och medlemmar
- Låna och returnera böcker
- Sök efter böcker (titel, författare, ISBN)
- Sortera böcker (alfabetiskt eller efter år)
- Se statistik (totalt antal böcker, utlånade böcker, mest aktiva låntagare)
- 15 enhetstester med xUnit

## Så här kör du programmet

### Förutsättningar
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) eller senare
  
-https://github.com/Alirepa/LibarySystem.git
cd LibrarySystem
dotnet build
cd LibrarySystem.Console
dotnet run
=== Bibliotekssystem ===
1. Visa alla böcker
2. Sök bok
3. Låna bok
4. Returnera bok
5. Visa medlemmar
6. Statistik
0. Avsluta

Välj: 
   
