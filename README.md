# Bibliotekssystem 2.0

Ett bibliotekssystem för hantering av böcker, medlemmar och utlåning - nu med databas och webbgränssnitt.

## Kort beskrivning av lösningen

Systemet är byggt med objektorienterade principer i C# och använder **komposition** (Alternativ B) där ett huvudbibliotek (`Library`) innehåller tre hanteringsklasser:
- `BookCatalog` - hanterar böcker
- `MemberRegistry` - hanterar medlemmar
- `LoanManager` - hanterar utlåning

### Del 1 - Konsolapplikation
**Huvudfunktioner:**
- Skapa böcker och medlemmar
- Låna och returnera böcker
- Sök efter böcker (titel, författare, ISBN)
- Sortera böcker (alfabetiskt eller efter år)
- Se statistik (totalt antal böcker, utlånade böcker, mest aktiva låntagare)
- 20 enhetstester med xUnit

### Del 2 - Webb och databas
**Nya funktioner:**
- **Entity Framework Core** med SQLite-databas
- Databasmodell med relationer (Books, Members, Loans)
- **Blazor Server** webbgränssnitt med:
  - Boklista med sökning och sortering
  - Medlemslista med aktiva lån
  - Lånelista med markering av försenade lån
  - Bokdetaljer med lånehistorik
  - Formulär med validering
- 10 nya enhetstester 

## Databasmodell

### Tabellstruktur

**Books**
| Kolumn | Typ | Beskrivning |
|--------|-----|-------------|
| Id | int (PK) | Primärnyckel |
| ISBN | string (unik) | ISBN-nummer |
| Title | string | Boktitel |
| Author | string | Författare |
| PublishedYear | int | Publiceringsår |
| IsAvailable | bool | Tillgänglig status |

**Members**
| Kolumn | Typ | Beskrivning |
|--------|-----|-------------|
| Id | int (PK) | Primärnyckel |
| MemberId | string (unik) | Medlems-ID |
| Name | string | Namn |
| Email | string (unik) | E-post |
| MemberSince | DateTime | Registreringsdatum |

**Loans**
| Kolumn | Typ | Beskrivning |
|--------|-----|-------------|
| Id | int (PK) | Primärnyckel |
| BookId | int (FK) | Länk till Books |
| MemberId | int (FK) | Länk till Members |
| LoanDate | DateTime | Lånedatum |
| DueDate | DateTime | Återlämningsdatum |
| ReturnDate | DateTime? | Faktiskt returdatum |

### Relationer
- En **Book** kan ha många **Loans**
- En **Member** kan ha många **Loans**
- Ett **Loan** tillhör en Book och en Member

## Skärmdumpar

### Boklista
<img width="861" height="349" alt="image" src="https://github.com/user-attachments/assets/8e42a28d-d98b-4d14-8526-0de9156460aa" />

*Sökning, sortering och status (Tillgänglig/Utlånad)*

### Bokdetaljer
<img width="494" height="605" alt="image" src="https://github.com/user-attachments/assets/09877097-d9dc-4209-a5cf-59b32506cd4b" />

*Lånehistorik och låna/returnera*

### Medlemmar 
<img width="761" height="278" alt="image" src="https://github.com/user-attachments/assets/2ad5757b-69dd-4bfd-8e71-dc92ec26a2c7" />
*Medlemslista med detaljer och att lägga till nya medlemmar*

### Lånelista
<img width="722" height="181" alt="image" src="https://github.com/user-attachments/assets/f1a8b4b5-b537-4935-b69b-ea05cba75327" />

*Aktiva lån med markering av försenade*

## Så här kör du programmet

### Förutsättningar
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) eller senare

### Köra webbapplikationen (Del 2)
```bash
# Klona repositoryt
git clone https://github.com/Alirepa/LibarySystem.git
cd LibrarySystem

# Bygg och kör webbapplikationen
cd LibrarySystem.Web
dotnet run

   
