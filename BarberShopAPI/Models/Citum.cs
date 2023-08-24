using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace BarberShopAPI.Models
{
    public partial class Citum
    {
        public Citum() 
        {
            Active = true;
        }

        public int CitaID { get; set; }
        public string? Description { get; set; }
        public DateTime Fecha { get; set; }
        public string? Hora { get; set; }
        public bool? Active { get; set; }
        public int UserId { get; set; }
        public int ClienteId { get; set; }
        public int CategoriaCitaId { get; set; }

        public virtual CategoriaCitum? CategoriaCitaCategoriaCitaNavigation { get; set; } = null!;
        public virtual Cliente? ClienteCliente { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
