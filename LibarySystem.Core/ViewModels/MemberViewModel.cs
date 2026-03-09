using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Medlems-ID är obligatoriskt")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Medlems-ID måste vara mellan 3-10 tecken")]
        [Display(Name = "Medlems-ID")]
        public string MemberId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Namn måste vara mellan 2-100 tecken")]
        [Display(Name = "Namn")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-post är obligatoriskt")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        [StringLength(100, ErrorMessage = "E-post får max vara 100 tecken")]
        [Display(Name = "E-post")]
        public string Email { get; set; } = string.Empty;
    }
}
