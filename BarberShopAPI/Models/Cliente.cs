using System;
using System.Collections.Generic;

namespace BarberShopAPI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cita = new HashSet<Citum>();
            Active = true;
        }

        public int ClienteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Correo { get; set; }
        public string? Celular { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Citum> Cita { get; set; }
    }
}
