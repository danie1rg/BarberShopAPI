using System;
using System.Collections.Generic;

namespace BarberShopAPI.Models
{
    public partial class CategoriaCitum
    {
        public CategoriaCitum()
        {
            Cita = new HashSet<Citum>();
        }

        public int CategoriaCitaID { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Citum> Cita { get; set; }
    }
}
