using System.ComponentModel.DataAnnotations;

namespace StiVisita.Models
{  
    public class Admission
    {  
        public int Id { get; set; }

        [Display(Name = "Cód. da Admissão")]
        public string? AdmissionCode { get; set; }

        [Display(Name = "Doente")]
        [DataType(DataType.Text)]
        public int? PatientId { get; set; }
        
        [Display(Name = "Nome do Doente")]
        public string? PatientName { get; set; }

        [Display(Name = "Data da Admissão")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EntryDate { get; set; }
        
        [Display(Name = "Observações")]
        [Required]
        public string? Observation { get; set; }
    }
}