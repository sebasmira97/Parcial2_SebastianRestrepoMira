using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Parcial2_SebastianRestrepoMira.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        //[Display(Name = "Uso de Boleto")]
        public DateTime? UseDate { get; set; }
        public bool IsUsed { get; set; }
        public string? EntranceGate { get; set; }
    }
}
