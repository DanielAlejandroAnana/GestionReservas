using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace GestionReservas.Controllers
{
    [ApiController]
    [Route("api/[reserva]")]
    public class ReservasController : ControllerBase
    {
        private readonly ReservasDbContext _context;

        public ReservasController(ReservasDbContext context)
        {
            _context = context;
        }

        [HttpGet("servicios")]
        public IActionResult GetServicios()
        {
            var servicios = new List<string> { "Servicio 1", "Servicio 2", "Servicio 3" };
            return Ok(servicios);
        }

        [HttpGet("reservas")]
        public IActionResult GetReservas()
        {
            var reservas = _context.Reservas.Include(r => r.ClienteId).ToList();
            return Ok(reservas);
        }

        [HttpPost("reservar")]
        public IActionResult CrearReserva([FromBody] Reserva nuevaReserva)
        {
            if (_context.Reservas.Any(r => r.Fecha == nuevaReserva.Fecha && r.Hora == nuevaReserva.Hora))
            {
                return BadRequest("Ya existe una reserva para esa fecha y horario.");
            }

            if (_context.Reservas.Any(r => r.ClienteId == nuevaReserva.ClienteId && r.Fecha == nuevaReserva.Fecha))
            {
                return BadRequest("El cliente ya tiene una reserva ese día.");
            }

            _context.Reservas.Add(nuevaReserva);
            _context.SaveChanges();

            return Ok(new { Exito = true, Mensaje = "Reserva creada con éxito." });
        }
    }
}
