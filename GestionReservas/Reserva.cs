using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionReservas
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Servicio { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora{ get; set; }
    }
}
