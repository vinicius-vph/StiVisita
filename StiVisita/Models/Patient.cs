using System.ComponentModel.DataAnnotations;

namespace StiVisita.Models
{
    public class Patient
    {
        public int? Id { get; set; }

        [Display(Name = "Cód. do Doente")]
        public string? PatientCode { get; set; }

        [Display(Name = "Nome do Doente")]
        [Required]
        public string? Name { get; set; }
    }
}
