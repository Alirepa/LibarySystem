using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace LibarySystem.Core.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ISBN är obligatoriskt")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN måste vara mellan 10-20 tecken")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Titel är obligatoriskt")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Titeln måste vara mellan 1-200 tecken")]
        [Display(Name = "Titel")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Författare är obligatoriskt")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Författarnamn måste vara mellan 2-100 tecken")]
        [Display(Name = "Författare")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publiceringsår är obligatoriskt")]
        [Range(1000, 2100, ErrorMessage = "Publiceringsår måste vara mellan 1000 och 2100")]
        [Display(Name = "Publiceringsår")]
        public int PublishedYear { get; set; } = DateTime.Now.Year;

        public bool IsAvailable { get; set; } = true;
    }
}
